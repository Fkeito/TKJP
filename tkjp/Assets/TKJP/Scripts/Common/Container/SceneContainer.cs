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

        public static void BindConextObj<T>(T obj)
        {
            if (Instance == null) return;
            Instance.Bind(obj);
        }
        public void Bind<T>(T obj)
        {
            ObjList.Add(typeof(T), obj);
        }
        public static T ContextObj<T>() where T : class
        {
            return Instance?.GetContextObj<T>();
        }
        public T GetContextObj<T>() where T : class
        {
            var obj = (T)ObjList[typeof(T)] ?? null;
            if(obj == null)
            {
                Debug.LogError(typeof(T).Name + "は、バインドされていません");
            }
            return obj;
        }
    }
}