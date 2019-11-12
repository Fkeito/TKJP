using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;

namespace TKJP.Battle.Game
{
    public class ResultManager : MonoBehaviour
    {
        private Result result;

        public Transition transition;
        private float transitionTime = 5f;
        private float time;

        public GameObject win;
        public GameObject lose;

        void OnEnable()
        {
            switch (result)
            {
                case Result.Win:
                    win.SetActive(true);
                    break;
                case Result.Lose:
                    lose.SetActive(true);
                    break;
            }
            time = 0f;
        }

        void Update()
        {
            time += Time.deltaTime;
            if(time > transitionTime)
            {
                transition.Load();
            }
        }

        public void SetResult(Result result)
        {
            this.result = result;
        }
    }
    public enum Result
    {
        Win,
        Lose,
    }

}