using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Battle.State
{
    public class ResultState : IState
    {
        public ResultState(GameObject view)
        {
            this.view = view;
        }

        private GameObject view;

        public void Initialize()
        {

        }

        public void Start()
        {
            view.SetActive(true);
        }

        public void Update()
        {

        }

        public bool IsFinish()
        {
            return false;
        }

        public void NextTo()
        {
            view.SetActive(false);
        }
    }
}
