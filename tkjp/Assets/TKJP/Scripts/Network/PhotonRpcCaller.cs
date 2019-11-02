using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Photon.Pun;
using Unity.Collections.LowLevel.Unsafe;

public class PhotonRpcCaller : MonoBehaviour
{
    public static PhotonRpcCaller Singleton;
    private PhotonView _PhotonView;
    

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