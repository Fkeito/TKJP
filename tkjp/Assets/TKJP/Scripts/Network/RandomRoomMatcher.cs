using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RandomRoomMatcher : MonoBehaviourPunCallbacks
{
    private bool isRoomMakeable = false;
    private bool isRoomJoin = false;
    public Text tex;
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

        if (isRoomJoin)
        {
            tex.text = "接続したよー";
        }
    }
    
    public override void OnConnectedToMaster()
    {
        isRoomMakeable = true;
    }

    public override void OnJoinedRoom()
    {
        isRoomJoin = true;
    }
}
