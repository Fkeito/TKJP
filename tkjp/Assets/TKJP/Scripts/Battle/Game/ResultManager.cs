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

        void OnEnable()
        {
            //Todo: 表示を変える
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