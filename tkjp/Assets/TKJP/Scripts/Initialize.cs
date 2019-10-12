using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace TKJP
{
    public class Initialize : MonoBehaviour
    {
        private const string DEVICE_KEY = "PLATFORM_DEVICE_KEY";
        public GameObject platformCanvas;

        void Awake()
        {

        }

        void Start()
        {
#if UNITY_EDITOR
            Settings.device = Settings.Device.Editor;
#elif UNITY_ANDROID
            string platform = PlayerPrefs.GetString(DEVICE_KEY, "");
            if (string.IsNullOrWhiteSpace(platform))
            {
                platformCanvas.SetActive(true);
            }
            else
            {
                SetPlatform(platform, false);
            }
#endif
        }

        public void SelectedPlatform(int platform)
        {
            SetPlatform(platform);
            platformCanvas.SetActive(false);
        }

        private void SetPlatform(string platform, bool strictly = true)
        {
            switch (platform)
            {
                case "Quest":
                    Settings.device = Settings.Device.Quest;
                    break;
                case "Go":
                    Settings.device = Settings.Device.Go;
                    break;
                case "Editor":
                    Settings.device = Settings.Device.Editor;
                    break;
                case "Else":
                    Settings.device = Settings.Device.Else;
                    break;
                default:
                    if(strictly) Settings.device = Settings.Device.Else;
                    else Settings.device = Settings.Device.None;
                    break;
            }
        }
        private void SetPlatform(int platform, bool strictly = true)
        {
            if (platform < 0 || platform > System.Enum.GetNames(typeof(Settings.Device)).Length)
            {
                Settings.device = (Settings.Device)platform;
            }
            else
            {
                if(strictly) Settings.device = Settings.Device.Else;
                else Settings.device = Settings.Device.None;
            }
        }
    }
}
