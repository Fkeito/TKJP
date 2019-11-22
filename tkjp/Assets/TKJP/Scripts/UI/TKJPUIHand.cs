using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Player;

namespace TKJP.UI
{
    public class TKJPUIHand : MonoBehaviour
    {
        // Grip trigger thresholds for picking up objects, with some hysteresis.
        public float grabBegin = 0.55f;
        public float grabEnd = 0.35f;
        private float prevFlex;

        private ITKJPTouch touch;
        private ITKJPGrab grab;

        public TKJPController.HandType handType;
        private bool isEditor;

        private void Start()
        {
            isEditor = Common.Settings.device == Common.Settings.Device.Editor;
        }

        private void Update()
        {
            float _prev = prevFlex;
            if (isEditor) prevFlex = Input.GetMouseButton(0) ? 1f : 0f;
            else prevFlex = OVRInput.Get(handType == TKJPController.HandType.Right ? OVRInput.Axis1D.SecondaryHandTrigger : OVRInput.Axis1D.PrimaryHandTrigger);
            CheckForGrabOrRelease(_prev);
        }

        void OnTriggerEnter(Collider other)
        {
            touch = other.GetComponent<ITKJPTouch>();
            touch?.OnTouchBegin();
            grab = other.GetComponent<ITKJPGrab>();
            //Todo: HandModel透明化
        }
        void OnTriggerStay(Collider other)
        {
            touch?.OnTouch();
        }
        private void OnTriggerExit(Collider other)
        {
            touch?.OnTouchEnd();
            touch = null;
            grab = null;
            //HandModel透明化解除
        }

        protected void CheckForGrabOrRelease(float prev)
        {
            if ((prevFlex >= grabBegin))
            {
                if (prev < grabBegin) grab?.OnGrabBegin();
                grab?.OnGrab();
            }
            else if ((prevFlex <= grabEnd) && (prev > grabEnd))
            {
                grab?.OnGrabEnd();
            }
        }
    }
}
