﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class JankenManager : MonoBehaviour, IState
    {
        private BattleManager battle;

        private JankenHand masterHand = JankenHand.None;
        private JankenHand clientHand = JankenHand.None;

        private bool isDraw;

        //public TimeEvent onTimeChanged;
        //public UnityEvent onTimeFinished;

        //private float jankenTime;
        //private float time;

        private bool jadging;
        private bool next;

        public void Initialize()
        {
            //onTimeChanged = new TimeEvent();

            //jankenTime = 3f;
            //time = 0f;
            next = false;
            jadging = false;
            isDraw = false;

            battle = Manager.GetState<BattleManager>();
        }

        public void OnChanged()
        {
            //time = 0f;
            next = false;
            jadging = false;
            isDraw = false;

            masterHand = JankenHand.None;
            clientHand = JankenHand.None;
        }

        public void OnUpdate()
        {
            //if (time <= jankenTime)
            //{
            //    time += Time.deltaTime;
            //    onTimeChanged.Invoke(time);
            //}
            //else if (!jadging)
            //{
            //    jadging = true;
            //    Debug.Log("Jadge" + clientHand.ToString() + " vs. " + masterHand.ToString());
            //    Jadge();
            //}

            if(masterHand != JankenHand.None && clientHand != JankenHand.None && !jadging)
            {
                jadging = true;
                Debug.Log("Jadge -> " + clientHand.ToString() + " vs. " + masterHand.ToString());
                Jadge();
            }
        }

        public bool IsFinish()
        {
            return next;
        }

        public void NextTo()
        {
            Manager.NextTo(isDraw ? State.State.Janken : State.State.Battle);
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
            yield return new WaitForSeconds(1f);
            Debug.Log("Jadge -> " + result);
            if (result != 0)
            {
                battle.SetGrabType(result);
                next = true;
            }
            else
            {
                isDraw = true;
                next = true;
            }
        }

        public enum JankenHand
        {
            None = -1,
            Rock = 0,
            Sissors = 1,
            Paper = 2
        }
    }
}

