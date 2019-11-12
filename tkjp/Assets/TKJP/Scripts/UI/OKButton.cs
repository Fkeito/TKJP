using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.UI
{
    public class OKButton : TKJPButton
    {
        public Animator anim;

        void Start()
        {
            anim = this.GetComponent<Animator>();
        }

        public override void OnTouchBegin()
        {

        }
        public override void OnTouchEnd()
        {

        }
        public override void OnGrabBegin()
        {

        }
        public override void OnGrabEnd()
        {
            onClick.Invoke();
            anim.SetTrigger("clicl");
        }
    }
}

