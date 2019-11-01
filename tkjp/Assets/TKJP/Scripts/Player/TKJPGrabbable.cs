using UnityEngine;
using System.Collections.Generic;
namespace TKJP.Player
{
    public class TKJPGrabbable : OVRGrabbable
    {
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
    }
}
