using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Sabaiteikuuuu : MonoBehaviourPunCallbacks {
    void Start () {
        PhotonNetwork.ConnectUsingSettings ();
    }
    public override void OnConnectedToMaster () {
        Debug.Log ("接続成功");
        PhotonNetwork.JoinOrCreateRoom ("ハヘベ", new RoomOptions (), TypedLobby.Default);
    }
    public override void OnJoinedRoom () {
        Debug.Log ("マッチング成功");
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log ("Master");
        } else {
            Debug.Log ("Guest");
        }
        var v = new Vector3 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));
        PhotonNetwork.Instantiate ("GamePlayer", v, Quaternion.identity);
    }
}