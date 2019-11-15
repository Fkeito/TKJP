using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class JankenManager : MonoBehaviour, IState
    {
        private BattleManager battle;

        private JankenHand masterHand;
        private JankenHand clientHand;

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

            battle = Manager.GetState<BattleManager>();
        }

        public void OnChanged()
        {
            time = 0f;
            next = false;
            jadging = false;
        }

        public void OnUpdate()
        {
            if (time <= jankenTime)
            {
                time += Time.deltaTime;
                onTimeChanged.Invoke(time);
            }
            else if (!jadging)
            {
                jadging = true;
                Debug.Log("Jadge" + clientHand.ToString() + " vs. " + masterHand.ToString());
                Jadge();
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
            Manager.NextTo(State.State.Battle);
        }

        public void SetJankenHand(int i)
        {
            masterHand = (JankenHand)i;
            Debug.Log("master hand is " + i);
        }
        public void SetClientJankenHand(int i)
        {
            clientHand = (JankenHand)i;
            Debug.Log(" hand is " + i);
        }
        public void Jadge()
        {
            StartCoroutine(_Jadge());
        }
        private IEnumerator _Jadge()
        {
            int result = (masterHand - clientHand + 3) % 3;
            //Todo: 演出
            yield return null;
            if (result != 0)
            {
                battle.SetGrabType(result);
                next = true;
            }
            else
            {
                OnChanged();
            }
        }

        public enum JankenHand
        {
            rock = 0,
            sissors = 1,
            paper = 2
        }
    }
}

