using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.UI
{
    public abstract class TKJPUI : MonoBehaviour, ITKJPGrab, ITKJPTouch
    {
        public virtual void OnTouchBegin() { }
        public virtual void OnTouch() { }
        public virtual void OnTouchEnd() { }
        public virtual void OnGrabBegin() { }
        public virtual void OnGrab() { }
        public virtual void OnGrabEnd() { }
    }
}
