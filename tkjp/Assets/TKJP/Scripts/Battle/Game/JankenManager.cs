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

        public Battle.Game.JankenHand jankenhand;

        private JankenHand masterHand = JankenHand.None;
        private JankenHand clientHand = JankenHand.None;

        private bool isDraw;

        private bool jadging;
        private bool next;

        public void Initialize()
        {
            battle = Manager.GetState<BattleManager>();
        }

        public void OnChanged()
        {
            next = false;
            jadging = false;
            isDraw = false;

            masterHand = JankenHand.None;
            clientHand = JankenHand.None;
        }

        public void OnUpdate()
        {
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
            jankenhand.SetMyHand(i+1);
            masterHand = (JankenHand)i;
        }

        public void SetClientJankenHand(int i)
        {
            clientHand = (JankenHand)i;
        }

        public void Jadge()
        {
            StartCoroutine(_Jadge());
        }
        private IEnumerator _Jadge()
        {
            jankenhand.SetEnemyHand((int)clientHand + 1);
            int result = (masterHand - clientHand + 3) % 3;
            //Todo: 演出
            yield return new WaitForSeconds(2f);
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

