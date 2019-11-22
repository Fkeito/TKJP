using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TKJP.Battle.Game;

namespace TKJP.Battle.State
{
    public class Manager : MonoBehaviour
    {
        private static Manager instance;

        private IState[] stateList;
        private int stateCount;
        private static State currentState;

        public ReadyManager ready;
        public JankenManager janken;
        public BattleManager battle;
        public ResultManager result;
        private GameObject[] views;

        public class StateEvent : UnityEvent<State> { }
        public StateEvent onChangeState = new StateEvent();

        void Awake()
        {
            if (instance != null) DestroyImmediate(this);
            instance = this;


            stateCount = System.Enum.GetValues(typeof(State)).Length;
            stateList = new IState[stateCount];

            SetStateList();

            foreach (IState state in stateList) state.Initialize();
            views = new GameObject[] { ready.gameObject, janken.gameObject, battle.gameObject, result.gameObject };

            currentState = 0;
            NextTo(currentState);
        }
        void Update()
        {
            IState current = stateList[(int)currentState];
            current.OnUpdate();
            if (current.IsFinish()) current.NextTo();
        }
        void OnDestroy()
        {
            if(instance == this)
            {
                instance = null;
            }
        }

        private void SetStateList()
        {
            stateList[(int)State.Ready] = ready;
            stateList[(int)State.Janken] = janken;
            stateList[(int)State.Battle] = battle;
            stateList[(int)State.Result] = result;
        }
        public static State GetCurrentState()
        {
            return currentState;
        }
        public static T GetState<T>() where T: class, IState
        {
            foreach(IState state in instance?.stateList)
            {
                if(state is T)
                {
                    return (T)state;
                }
            }

            return null;
        }
        public static GameObject GetStateObj(State state)
        {
            switch (state)
            {
                case State.Ready:
                    return instance?.ready?.gameObject;
                case State.Janken:
                    return instance?.janken?.gameObject;
                case State.Battle:
                    return instance?.battle?.gameObject;
                case State.Result:
                    return instance?.result?.gameObject;
                default:
                    return null;
            }
        }

        public static void AddListenerOnChangeState(UnityAction<State> action)
        {
            if (instance) instance.onChangeState.AddListener(action);
        }
        public static void NextTo(State state)
        {
            if (instance)
            {
                currentState = state;
                instance.stateList[(int)state].OnChanged();
                foreach(GameObject view in instance.views)
                {
                    view.SetActive(false);
                }
                instance.views[(int)state].SetActive(true);

                instance.onChangeState.Invoke(state);
            }
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
