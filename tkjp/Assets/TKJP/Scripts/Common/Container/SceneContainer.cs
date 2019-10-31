using System;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Common.Container
{
    //このオブジェクトは最初からシーンに置いておく
    public class SceneContainer : MonoBehaviour
    {
        public static SceneContainer Instance;
        public static void SetInstance(SceneContainer container)
        {
            Instance = container;
        }

        private Dictionary<Type, object> ObjList { get; } = new Dictionary<Type, object>();

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            SetInstance(this);
        }

        public void Bind<T>(T obj)
        {
            ObjList.Add(typeof(T), obj);
        }
        public T GetContextObj<T>()
        {
            return (T)ObjList[typeof(T)];
        }
    }
}