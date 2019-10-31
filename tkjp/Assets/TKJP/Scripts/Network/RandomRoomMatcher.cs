using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RandomRoomMatcher : MonoBehaviourPunCallbacks
{
    private bool isRoomMakeable = false;
    public void ConnectStart()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinOrCreat()
    {
        if (isRoomMakeable)
        {
            PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
            Debug.Log("マッチ成功");
            isRoomMakeable = false;
        }
        else
        {
            Debug.Log("----------"); 
        }
    }
    
    public override void OnConnectedToMaster()
    {
        isRoomMakeable = true;
    }
}
