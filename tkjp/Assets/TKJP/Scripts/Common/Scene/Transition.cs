using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TKJP.Common.Scene
{
    //Todo: typeごとに表示が変わるエディタ拡張書く
    public class Transition : MonoBehaviour
    {
        //共通部分
        public string loadSceneName;
        private bool loaded = false;
        public bool useAsync = false;
        private AsyncOperation async;

        //loadの条件関連
        public enum Type { OnlyEvent, Timeleft, Immidiate }
        public Type type;
        private ITransitionTerm loadable;
        //Timeleft用
        public float loadTime;

        public void StartLoadAsync()
        {
            if (loaded) return;
            if (async != null)
            {
                async = SceneManager.LoadSceneAsync(loadSceneName);
                async.allowSceneActivation = false;
            }
        }
        public void Load()
        {
            if (loaded) return;
            if (useAsync)
            {
                if (async != null) SceneManager.LoadSceneAsync(loadSceneName);
                else async.allowSceneActivation = true;

                loaded = true;
            }
            else
            {
                SceneManager.LoadScene(loadSceneName);
                loaded = true;
            }
        }
        public void Load(string sceneName)
        {
            if (loaded) return;

            SceneManager.LoadScene(loadSceneName);
            loaded = true;
        }


        private void Start()
        {
            switch (type)
            {
                case Type.Timeleft:
                    loadable = new LoadOnTimeleft(loadTime);
                    break;
                case Type.Immidiate:
                    loadable = new LoadOnTimeleft(0f);
                    break;
                case Type.OnlyEvent:
                    loadable = null;
                    break;
            }
        }
        private void LateUpdate()
        {
            if (loaded) return;
            if(loadable?.CheckUpdate() ?? false)
            {
                Load();
            }
        }
    }

    public class LoadOnTimeleft: ITransitionTerm
    {
        private float timeleft;
        public bool CheckUpdate()
        {
            timeleft -= Time.deltaTime;
            if(timeleft < 0f)
            {
                return true;
            }

            return false;
        }

        public LoadOnTimeleft(float time)
        {
            timeleft = time;
        }
    }
}