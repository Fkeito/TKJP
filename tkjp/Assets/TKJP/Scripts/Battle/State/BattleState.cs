using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Battle.State
{
    public class BattleState : IState
    {
        public TimeEvent onTimeChanged;

        private bool isSettled;
        private float battleTime;
        private float time;

        public void Initialize()
        {
            onTimeChanged = new TimeEvent();
            isSettled = false;
            battleTime = 5f;
            time = 0f;
        }

        public void Start()
        {
            time = 0f;
        }

        public void Update()
        {
            time += Time.deltaTime;
            onTimeChanged.Invoke(time);
        }

        public bool IsFinish()
        {
            return time > battleTime || isSettled;
        }

        public void NextTo()
        {
            Manager.NextTo(isSettled ? State.Result : State.Janken);
        }

        public void BeSettled()
        {
            isSettled = true;
        }
    }
}
