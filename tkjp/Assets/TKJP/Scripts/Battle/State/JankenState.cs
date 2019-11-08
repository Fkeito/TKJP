using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Battle.State
{
    public class JankenState: IState
    {
        public JankenState(GameObject view)
        {
            this.view = view;
        }

        private GameObject view;

        public TimeEvent onTimeChanged;

        private float jankenTime;
        private float time;
        private bool next;

        public void Initialize()
        {
            onTimeChanged = new TimeEvent();

            jankenTime = 3f;
            time = 0f;
            next = false;
        }

        public void Start()
        {
            time = 0f;
            next = false;
            view.SetActive(true);
        }

        public void Update()
        {
            time += Time.deltaTime;
            onTimeChanged.Invoke(time);
        }

        public bool IsFinish()
        {
            return time > jankenTime;
        }

        public void NextTo()
        {
            view.SetActive(false);
            Manager.NextTo(State.Battle);
        }
    }
}
