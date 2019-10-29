using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace TKJP.Common
{
    public class Initialize : MonoBehaviour
    {
        void Awake()
        {

        }

        void Start()
        {
            CheckEntitlement();
            InitSettings();
        }

        private void CheckEntitlement()
        {
            //Todo: 頑張る
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
    }
}
