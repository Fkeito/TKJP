using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TKJP.Common.Scene;
using Oculus.Platform;

namespace TKJP.Common
{
    public class Initialize : MonoBehaviour
    {
        private const string APP_ID = "2749973231700455";
        private bool finishEntitlement = false;
        private float splashTime = 5f;

        public bool checkEntitlement = true;

        void Start()
        {
            if (checkEntitlement) CheckEntitlement();
            else
            {
                finishEntitlement = true;
                InitSettings();
            }
        }
        void Update()
        {
            splashTime -= Time.deltaTime;

            if(splashTime < 0 && finishEntitlement)
            {
                Transition t = this.GetComponent<Transition>();
                t.Load();
            }
        }

        private void CheckEntitlement()
        {
            Core.AsyncInitialize(APP_ID);
            Entitlements.IsUserEntitledToApplication().OnComplete(OnFinishEntitlementCheck);
        }
        private void InitSettings()
        {
            OVRPlugin.SystemHeadset headset = OVRPlugin.GetSystemHeadsetType();
            switch (headset)
            {
                case OVRPlugin.SystemHeadset.Oculus_Go:
                    Settings.device = Settings.Device.Go;
                    break;
                case OVRPlugin.SystemHeadset.Oculus_Quest:
                    Settings.device = Settings.Device.Quest;
                    break;
                default:
#if UNITY_EDITOR
                    Settings.device = Settings.Device.Editor;
#else
                    Settings.device = Settings.Device.Else;
#endif
                    break;
            }
        }

        private void OnFinishEntitlementCheck(Message msg)
        {
            if (msg.IsError)
            {
                //entitlement check is failed
                return;
            }

            InitSettings();
            finishEntitlement = true;
        }
    }
}
