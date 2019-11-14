using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class ReadyManager : MonoBehaviour
    {
        private ReadyState state;
        public Transition transition;

        void Start()
        {
            state = Manager.GetState<ReadyState>();
        }

        public void IsReady()
        {
            state.MasterGetReady();
        }
        public void BackMenu()
        {
            transition.Load();
        }
    }
}
