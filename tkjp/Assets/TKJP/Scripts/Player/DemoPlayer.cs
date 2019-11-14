using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Battle.State;
using TKJP.Battle.Game;

namespace TKJP.Player
{
    public class DemoPlayer : MonoBehaviour
    {
        private State preState;

        void Start()
        {
            State state = Manager.GetCurrentState();
            OnSwitchedState(state);
            preState = state;
        }
        void Update()
        {
            State state = Manager.GetCurrentState();
            if (state != preState) OnSwitchedState(state);
            preState = state;
        }

        void OnSwitchedState(State state)
        {
            switch (state)
            {
                case State.Ready:
                    Manager.GetState<ReadyState>().ClientGetReady();
                    break;
                case State.Janken:
                    Manager.GetStateObj(Manager.GetCurrentState()).GetComponent<JankenManager>().SetClientHand(Random.Range(0, 3));
                    break;
                case State.Battle:
                    break;
                case State.Result:
                    break;
            }
        }
    }

}