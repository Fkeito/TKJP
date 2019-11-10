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
        private Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
        private Color clickedColor = new Color(0.5f, 0.5f, 0.5f);

        void Start()
        {
            sprite = this.GetComponent<SpriteRenderer>();
            sprite.color = defaultColor;
        }

        public override void OnTouchBegin()
        {
            //Todo: バイブレーション
            sprite.color = hoverColor;
        }
        public override void OnTouchEnd()
        {
            sprite.color = defaultColor;
        }
        public override void OnGrabBegin()
        {
            sprite.color = clickedColor;
        }
        public override void OnGrabEnd()
        {
            onClick.Invoke();
            sprite.color = defaultColor;
        }
    }
}