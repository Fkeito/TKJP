using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CubeController : MonoBehaviourPunCallbacks
{

//
//    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
//        rb = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float dx = Input.GetAxis("Horizontal");
            float dy = Input.GetAxis("Vertical");
            transform.Translate(dx, dy, 0f);
        }
    }
}