using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common;

namespace TKJP.Player
{
    public class TKJPGoController : MonoBehaviour
    {
        public Transform handModel;
        private Settings.Device device;

        public float reachableDistance;
        private float prevDistance;

        private void Awake()
        {
            //debug用
            if(OVRPlugin.GetSystemHeadsetType() == OVRPlugin.SystemHeadset.Oculus_Go) Settings.device = Settings.Device.Go;

        }

        void Start()
        {
            device = Settings.device;

            switch (device)
            {
                case Settings.Device.Quest:
                    Destroy(this);
                    break;
                case Settings.Device.Go:
                    Destroy(this);
                    break;
                default:
                    //this.transform.position = new Vector3(1f, -1f, 1f);
                    break;
            }
        }

        void Update()
        {
            SetModelPos();
        }

        void SetModelPos()
        {
            if (device != Settings.Device.Go)
            {
                Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit cameraHit;
                Physics.Raycast(cameraRay, out cameraHit, reachableDistance);
                if (cameraHit.distance > 0f) handModel.transform.forward = (cameraHit.point - this.transform.position).normalized;
                else handModel.transform.localRotation = Quaternion.identity;

                Ray ray = new Ray(this.transform.position, handModel.forward);
                RaycastHit hit;

                Physics.Raycast(ray, out hit, reachableDistance);

                float distance = Mathf.Lerp(prevDistance, hit.distance, 0.1f);
                handModel.localPosition = handModel.forward * distance;
                prevDistance = distance;
            }
        }
    }
}
