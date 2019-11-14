using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkTransform : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks,IPunObservable
{
    private PhotonView photonview;
    private void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        Debug.Log("Request");
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Transfered");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log(info.Sender);
    }
    public void GetOwnershipButtonEvent()
    {
        photonview.RequestOwnership();
    }
}
