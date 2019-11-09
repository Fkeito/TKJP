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
