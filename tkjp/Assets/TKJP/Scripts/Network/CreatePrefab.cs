using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class CreatePrefab : MonoBehaviour
{
    private void Start()
    {
        //_photonRpcCaller = GetComponent<PhotonRpcCaller>();
    }

    public void InstantiateCube()
    {
        PhotonRpcCaller.Singleton.CallCreateAction("Cube");
    }
}
