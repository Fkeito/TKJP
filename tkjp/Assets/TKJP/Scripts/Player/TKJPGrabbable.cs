using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TKJP.Player
{
    public class TKJPGrabbable : OVRGrabbable, IDelatable
    {
        public GameObject deleteEffect;
        public float deleteEffectTime;
        public MeshRenderer mesh;

        private List<Component> Components = new List<Component>();
        protected T AddComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (component != null) return component;
            component = gameObject.AddComponent<T>();
            Components.Add(component);
            return component;
        }

        protected void RemoveComponent<T>() where T : Component
        {
            RemoveComponent(gameObject.GetComponent<T>());
        }
        private void RemoveComponent(Component component)
        {
            if (component == null) return;
            Components.Remove(component);
            Destroy(component);
        }

        public virtual void Init()
        {
            for(int i = Components.Count - 1;i>= 0; i--)
            {
                RemoveComponent(Components[i]);
            }
        }

        void OnEnabled()
        {
            m_allowOffhandGrab = true;
        }

        public void Delete()
        {
            StartCoroutine(SetDeleteEffect());
        }
        private IEnumerator SetDeleteEffect()
        {
            m_allowOffhandGrab = false;
            float deleteTime = 0.5f;
            float time = 0;
            Color color = mesh.material.color;
            yield return null;
            while(time < deleteTime)
            {
                float albedo = (deleteTime - time) / deleteTime;
                color.a = albedo;
                mesh.material.color = color;
            }
            color.a = 0;
            mesh.material.color = color;
            deleteEffect.SetActive(true);
            yield return new WaitForSeconds(deleteEffectTime);
            color.a = 1;
            mesh.material.color = color;
            deleteEffect.SetActive(true);
        }
    }
}
