using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class JankenManager : MonoBehaviour
    {
        private BattleManager battle;

        private int masterHand { get { return masterHand; } set { masterHand = value % 3; } }
        private int clientHand { get { return clientHand; } set { clientHand = value % 3; } }

        void Start()
        {
            var state = State.Manager.GetState<JankenState>();

            battle = State.Manager.GetStateObj(State.State.Battle).GetComponent<BattleManager>();

            state.onTimeFinished.AddListener(Jadge);
        }

        void OnEnable()
        {
            masterHand = clientHand = 0;
        }

        public void SetHand(int i)
        {
            masterHand = i;
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
            battle.SetGrabType(result);
        }
    }
}

