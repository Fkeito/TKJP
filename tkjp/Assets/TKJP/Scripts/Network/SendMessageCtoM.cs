using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SendMessageCtoM : MonoBehaviour
{
    public void SendReady()
    {
        PhotonRpcCaller.Singleton.CallReadyAction();
    }
    public void SendJanken()
    {
        PhotonRpcCaller.Singleton.CallJankenAction();
    }
    public void SendBattle()
    {
        PhotonRpcCaller.Singleton.CallBattleAction(4.0f);
    }
    public void SendResult()
    {
        PhotonRpcCaller.Singleton.CallResultAction();
    }
}
