using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Battle.State
{
    public class Manager : MonoBehaviour
    {
        private static IState[] stateList;
        private static int stateCount;
        private static State currentState;

        public GameObject readyView;
        public GameObject jankenView;
        public GameObject battleView;
        public GameObject resultView;

        void Awake()
        {
            stateCount = System.Enum.GetValues(typeof(State)).Length;
            stateList = new IState[stateCount];

            SetStateList();

            foreach (IState state in stateList) state.Initialize();

            currentState = 0;
            stateList[(int)currentState].Start();
        }
        void Update()
        {
            IState current = stateList[(int)currentState];
            current.Update();
            if (current.IsFinish()) current.NextTo();
        }

        private void SetStateList()
        {
            stateList[(int)State.Ready] = new ReadyState(readyView);
            stateList[(int)State.Janken] = new JankenState(jankenView);
            stateList[(int)State.Battle] = new BattleState(battleView);
            stateList[(int)State.Result] = new ResultState(resultView);
        }
        public static T GetState<T>() where T: class, IState
        {
            foreach(IState state in stateList)
            {
                if(state is T)
                {
                    return (T)state;
                }
            }

            return null;
        }

        public static void NextTo(State state)
        {
            currentState = state;
            stateList[(int)state].Start();
        }
    }

    public enum State
    {
        Ready = 0,
        Janken = 1,
        Battle = 2,
        Result = 3
    }
}
