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

            Manager.AddListenerOnChangeState((s) => OnSwitchedState(s));
        }

        void OnSwitchedState(State state)
        {
            switch (state)
            {
                case State.Ready:
                    StartCoroutine(DelayMethod(1f, () => Manager.GetState<ReadyManager>().ClientGetReady()));
                    break;
                case State.Janken:
                    StartCoroutine(DelayMethod(1f, () => Manager.GetState<JankenManager>().SetClientJankenHand(Random.Range(0, 3))));
                    break;
                case State.Battle:
                    break;
                case State.Result:
                    break;
            }
        }
        private IEnumerator DelayMethod(float delayTime, System.Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action();
        }
    }

}