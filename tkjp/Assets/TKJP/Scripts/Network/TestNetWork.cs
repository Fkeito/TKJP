using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Debug = TKJP.Common.Debug;

public class TestNetWork : MonoBehaviour
{
    private PhotonRpcCaller _rpcCaller;

    public void CallRpc()
    {
        if (PhotonRpcCaller.Singleton == null)
        {
            Debug.Log("空ですよ");
            return;
        }
        PhotonRpcCaller.Singleton.RPCToOthers("CallDebug","やったぜ，繋がった");
    }
    
    [PunRPC]
    public void CallDebug(string msg)
    {
        Debug.Log(msg);
    }
}
