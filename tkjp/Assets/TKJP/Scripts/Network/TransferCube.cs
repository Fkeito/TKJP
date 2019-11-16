using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TransferCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferCubeiyeee()
    {
        Player otherPlayer = PhotonNetwork.PlayerListOthers[0];
        GameObject.Find("Cube(Clone)").GetComponent<PhotonView>().TransferOwnership(otherPlayer);
    }
}
