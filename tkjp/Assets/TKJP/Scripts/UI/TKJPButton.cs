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
        private SpriteRenderer sprite;

        private Color defaultColor = new Color(1f, 1f, 1f);
        private Color hoverColor = new Color(0.8f, 0.8f, 0.8f);
        private Color clickedColor = new Color(0.5f, 0.5f, 0.5f);

        protected virtual void Start()
        {
            sprite = this.GetComponent<SpriteRenderer>();
            if(sprite.sprite != null) sprite.color = defaultColor;
        }

        public override void OnTouchBegin()
        {
            //Todo: バイブレーション
            if (sprite.sprite != null) sprite.color = hoverColor;
        }
        public override void OnTouchEnd()
        {
            if (sprite.sprite != null) sprite.color = defaultColor;
        }
        public override void OnGrabBegin()
        {
            if (sprite.sprite != null) sprite.color = clickedColor;
        }
        public override void OnGrabEnd()
        {
            onClick.Invoke();
            if (sprite.sprite != null) sprite.color = defaultColor;
        }
    }
}