using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using UniRx.Triggers;
using Unity.Collections.LowLevel.Unsafe;
using Debug = TKJP.Common.Debug;
using Random = UnityEngine.Random;

public class PhotonRpcCaller : MonoBehaviour
{
    public static PhotonRpcCaller Singleton;
    private PhotonView _PhotonView;
    public event Action <string> CreateAction = null;
    public event Action Ready = null;
    public event Action Janken = null;
    public event Action <float> Battle = null;
    public event Action Result = null;

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

    public void CallCreateAction(string str)
    {
        CreateAction(str);
    }
    public void CallReadyAction()
    {
        Ready();
    }
    public void CallJankenAction()
    {
        Janken();
    }
    public void CallBattleAction(float value)
    {
        Battle(value);
    }public void CallResultAction()
    {
        Result();
    }
    public void CallRpc()
    {
        this.RPCToOthers("CallStringDebug","やったぜ，繋がった");
    }
    [PunRPC]
    private void CallStringDebug(string msg)
    {
        Debug.Log(msg);
    }

    [PunRPC]
    private void CallValueDebug(float value)
    {
        Debug.Log(value * value + "計算結果");
    }
    
    public void CreateObj(string prefabId)
    {
        var v = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        PhotonNetwork.Instantiate(prefabId, v, Quaternion.identity);
    }

    public void SendMessageforReady()
    {
        this.RPCToOthers("CallStringDebug","SendMessageforReady準備完了");
    }

    public void SendCommandforJanken()
    {
        this.RPCToOthers("CallStringDebug","グーチョキパー");
    }

    public void SendBattleParameter(float value)
    {
        this.RPCToOthers("CallValueDebug","value");
    }

    public void SendMessageforResult()
    {
        this.RPCToOthers("CallStringDebug","結果を送信");
    }
    private void OnEnable()
    {
        //呼び出したいメソッドをEventに代入
        this.CreateAction += CreateObj;
        this.Ready += SendMessageforReady;
        this.Janken += SendCommandforJanken;
        this.Battle += SendBattleParameter;
        this.Result += SendMessageforResult;
    }

    private void OnDisable()
    {
        this.CreateAction -= CreateObj;
        this.Ready += SendMessageforReady;
        this.Janken += SendCommandforJanken;
        this.Battle += SendBattleParameter;
        this.Result += SendMessageforResult;
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