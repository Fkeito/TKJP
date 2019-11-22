using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Photon.Pun;

namespace TKJP.Common
{
    public static class Debug
    {
        [Conditional("UNITY_EDITOR")]
        //[PunRPC]
        public static void Log(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }
        [Conditional("UNITY_EDITOR")]
        //[PunRPC]
        public static void LogError(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }
        [Conditional("UNITY_EDITOR")]
        //[PunRPC]
        public static void LogWarning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }
    }
}
