using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TKJP.Battle.State
{
    public class JankenState: IState
    {
        public TimeEvent onTimeChanged;
        public UnityEvent onTimeFinished;

        private float jankenTime;
        private float time;

        private bool jadging;
        private bool next;

        public void Initialize()
        {
            onTimeChanged = new TimeEvent();

            jankenTime = 3f;
            time = 0f;
            next = false;
            jadging = false;
        }

        public void Start()
        {
            time = 0f;
            next = false;
            jadging = false;
        }

        public void Update()
        {
            if (time <= jankenTime)
            {
                time += Time.deltaTime;
                onTimeChanged.Invoke(time);
            }
            else if (!jadging)
            {
                onTimeFinished.Invoke();
                jadging = true;
            }
        }

        public void FinishJadging()
        {
            if (jadging)
            {
                next = true;
            }
        }

        public bool IsFinish()
        {
            return next;
        }

        public void NextTo()
        {
            Manager.NextTo(State.Battle);
        }
    }
}
