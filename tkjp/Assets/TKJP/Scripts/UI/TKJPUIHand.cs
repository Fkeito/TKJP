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
        private bool isGrab;
        private bool prevGrab;

        public TKJPController.HandType handType;

        private void Update()
        {
            float _prev = prevFlex;
            prevFlex = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            CheckForGrabOrRelease(_prev);
        }

        void OnTriggerEnter(Collider other)
        {
            ITKJPTouch touch = other.GetComponent<ITKJPTouch>();
            touch?.OnTouchBegin();
            //Todo: HandModel透明化
        }
        void OnTriggerStay(Collider other)
        {
            ITKJPTouch touch = other.GetComponent<ITKJPTouch>();
            touch?.OnTouch();

            ITKJPGrab grab = other.GetComponent<ITKJPGrab>();
            if (prevGrab != isGrab)
            {
                if (isGrab) grab?.OnGrabBegin();
                else grab?.OnGrabEnd();
            }
            if (isGrab)
            {
                grab?.OnGrab();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            ITKJPTouch touch = other.GetComponent<ITKJPTouch>();
            touch?.OnTouchEnd();
            isGrab = false;
            //HandModel透明化解除
        }

        protected void CheckForGrabOrRelease(float prev)
        {
            if ((prevFlex >= grabBegin) && (prev < grabBegin))
            {
                isGrab = true;
            }
            else if ((prevFlex <= grabEnd) && (prev > grabEnd))
            {
                isGrab = false;
            }
        }
    }
}
