using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using Unity.Collections.LowLevel.Unsafe;
using Debug = TKJP.Common.Debug;
using Random = UnityEngine.Random;

public class PhotonRpcCaller : MonoBehaviour
{
    public static PhotonRpcCaller Singleton;
    private PhotonView _PhotonView;
    public event Action <string> CreateAction = null;

    private void Awake()
    {
        Debug.Log("Awake");
        if (Singleton != null)
        {
            DestroyImmediate(this);
        }
        Singleton = this;
    }
    
    private void OnDestroy()
    {
        if (Singleton == this)
        {
            Singleton = null;
        }
    }

    private void Start()
    {
        _PhotonView = GetComponent<PhotonView>();
    }

    //_PhotonView.RPC(methodname);
    public void RPC(string methodName, RpcTarget target, params object[] parameters)
    {
        _PhotonView.RPC(methodName,target,parameters);
    }
    public void RPCToAllViaServer(string methodName, params object[] parameters)
    {
        RPCToAllViaServer(methodName, false, parameters);
    }

    public void RPCToAllViaServer(string methodName, bool bufferd, params object[] parameters)
    {
        if (bufferd)
        {
            _PhotonView.RPC(methodName, RpcTarget.AllBufferedViaServer, parameters);
        }
        else
        {
            _PhotonView.RPC(methodName, RpcTarget.AllViaServer, parameters);
        }
    }

    public void RPCToOthers(string methodName, params object[] parameters)
    {
        RPCToOthers(methodName, false, parameters);
    }

    public void RPCToOthers(string methodName, bool bufferd, params object[] parameters)
    {
        if (bufferd)
        {
            _PhotonView.RPC(methodName, RpcTarget.OthersBuffered, parameters);
        }
        else
        {
            _PhotonView.RPC(methodName, RpcTarget.Others, parameters);
        }
    }

    public void CallMethodAction(string str)
    {
        CreateAction(str);
        Debug.Log("Call Action");
    }
    public void CallRpc()
    {
        this.RPCToOthers("CallDebug","やったぜ，繋がった");
    }
    [PunRPC]
    private void CallDebug(string msg)
    {
        Debug.Log(msg);
    }

//    public void TestAAA()
//    {
//        this.RPCToOthers("CallDebug","よろしくお願いします！！");
//    }

    public void CreateObj(string prefabId)
    {
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate(prefabId, v, Quaternion.identity);
    }

    private void OnEnable()
    {
        //呼び出したいメソッドをEventに代入
        this.CreateAction += CreateObj;
    }

    private void OnDisable()
    {
        this.CreateAction -= CreateObj;
    }
}

/*
    this.RPC("メソッド名");
    を，PhotonViewを継承せずとも呼べるようにしたい。
    なので，代わりにPhotonRpcCallerがこれを呼ぶ
    
    外部では次のようにアクセスできると嬉しい
    
    void Start(){
        PhotonRpcCaller.Singleton.Rpc("メソッド名");
    }
*/