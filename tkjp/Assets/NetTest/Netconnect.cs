using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Netconnect : MonoBehaviour
{
    private bool join = false;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (!join)
            {
                PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
            }
        }
        if (PhotonNetwork.InRoom)
        {
            join = true;
            Debug.Log("aaaaaa");
        }
    }
}
