using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestNetWorkforClient : MonoBehaviour
{
    private PhotonView photonview;
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveOwnership();
        }
    }
    public void MoveOwnership()
    {
        Player otherPlayer = PhotonNetwork.PlayerListOthers[0];
        photonview.TransferOwnership(otherPlayer);
        Debug.Log("MoveOwnership to " + otherPlayer.UserId);
    }
}
