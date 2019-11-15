using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Battle.State;
using TKJP.Common.Scene;

namespace TKJP.Battle.Game
{
    public class ResultManager : MonoBehaviour, IState
    {
        private Result result;

        public Transition transition;
        private float transitionTime = 5f;
        private float time;

        public GameObject win;
        public GameObject lose;

        public void Initialize()
        {

        }

        public void OnChanged()
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

        public void OnUpdate()
        {
            time += Time.deltaTime;
            if (time > transitionTime)
            {
                transition.Load();
            }
        }

        public bool IsFinish()
        {
            return false;
        }

        public void NextTo()
        {

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