using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TKJP.UI
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class TKJPButton: TKJPUI
    {
        public UnityEvent onClick;

        public override void OnTouchBegin()
        {
            //Todo: バイブレーション
        }
        public override void OnGrabEnd()
        {
            onClick.Invoke();
        }
    }
}