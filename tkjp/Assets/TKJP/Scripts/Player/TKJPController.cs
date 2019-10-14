using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common;

namespace TKJP.Player
{
    public class TKJPController : MonoBehaviour
    {
        //接続してる時だけ手を有効にします feat.OVRControllerHelper

        public GameObject hand;
        [SerializeField]
        private HandType handType;
        [System.Serializable] private enum HandType { Left, Right, }

        private OVRInput.Controller controller = OVRInput.Controller.None;
        private Settings.Device device = Settings.Device.None;
        private bool prevControllerConnecnted = false;

        void Start()
        {
            device = Settings.device;
            Debug.Log(device);

            //接続確認用初期設定
            switch (device)
            {
                case Settings.Device.Quest:
                    controller = handType == HandType.Left ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
                    break;
                default:
                    controller = handType == HandType.Left ? OVRInput.Controller.LTrackedRemote : OVRInput.Controller.RTrackedRemote;
                    if (handType == HandType.Left)
                    {
                        hand.SetActive(false);
                    }
                    break;

            }
        }

        void Update()
        {
            bool controllerConnected = OVRInput.IsControllerConnected(controller);

            //接続状況が変わったときだけ変更;
            if(controllerConnected != prevControllerConnecnted)
            {
                Debug.Log(controllerConnected);
                hand.SetActive(controllerConnected);
                prevControllerConnecnted = controllerConnected;
            }
        }
    }
}