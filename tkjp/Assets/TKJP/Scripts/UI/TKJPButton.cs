using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TKJP.UI
{
    public class TKJPButton: TKJPUI
    {
        public UnityEvent onClick;
        private SpriteRenderer sprite;

        private Color defaultColor = new Color(1f, 1f, 1f);
        private Color clickedColor = new Color(0.75f, 0.75f, 0.75f);

        void Start()
        {
            sprite = this.GetComponent<SpriteRenderer>();
            sprite.color = defaultColor;
            sprite.material.SetFloat("_OutLineSpread", 0f);
        }

        public override void OnTouchBegin()
        {
            //Todo: バイブレーション
            sprite.material.SetFloat("_OutLineSpread", 0.02f);
        }
        public override void OnTouchEnd()
        {
            sprite.color = defaultColor;
            sprite.material.SetFloat("_OutLineSpread", 0f);
        }
        public override void OnGrabBegin()
        {
            sprite.color = clickedColor;
        }
        public override void OnGrabEnd()
        {
            onClick.Invoke();
            sprite.color = defaultColor;
            sprite.material.SetFloat("_OutLineSpread", 0f);
        }
    }
}