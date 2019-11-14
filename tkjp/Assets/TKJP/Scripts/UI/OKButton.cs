using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.UI
{
    public class OKButton : TKJPButton
    {
        public Animator anim;

        protected override void Start()
        {
            base.Start();
            anim = this.GetComponent<Animator>();
        }

        public override void OnTouchBegin()
        {
            //base.OnTouchBegin();
        }
        public override void OnTouchEnd()
        {
            //base.OnTouchEnd();
        }
        public override void OnGrabBegin()
        {
            //base.OnGrabBegin();
        }
        public override void OnGrabEnd()
        {
            //base.OnGrabEnd();
            onClick.Invoke();
            anim.SetTrigger("click");
        }
    }
}

