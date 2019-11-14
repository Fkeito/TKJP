using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetWorkTransform : MonoBehaviour, IPunObservable
{
    private PhotonView _photonView;

    public GameObject prefab;

    private PhotonView h;
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
//        if (_photonView.IsMine) {
//            Debug.Log("呼ばれてるよ");
//            var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
//            var dv = 6f * Time.deltaTime * direction;
//            transform.Translate(dv.x, dv.y, 0f);
//        }
//        else
//        {
//            Debug.Log("呼ばれてない");
//        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.transform.position);
        }
        else
        {
            transform.position = (Vector3) stream.ReceiveNext();
        }
    }
}
