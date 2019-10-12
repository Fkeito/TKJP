using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Sabaiteikuuuu : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;
    void Start () {
        PhotonNetwork.ConnectUsingSettings ();
        _photonView = GetComponent<PhotonView>();
    }
    public override void OnConnectedToMaster () {
        Debug.Log ("接続成功");
        PhotonNetwork.JoinOrCreateRoom("ハヘベ", new RoomOptions(), TypedLobby.Default);
    }
    public override void OnJoinedRoom () {
        Debug.Log ("マッチング成功");
        if (PhotonNetwork.IsMasterClient) {
            _photonView.RPC("CreateCube", RpcTarget.AllBufferedViaServer);
            Debug.Log ("Master");
        } else {
            Debug.Log ("Guest");
        }
    }

    [PunRPC]
    private void CreateCube()
    {
        var v = new Vector3 (Random.Range (-3f, 3f), Random.Range (-3f, 3f));
        PhotonNetwork.Instantiate ("GamePlayer", v, Quaternion.identity);
    }
}