using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class JankenHand : MonoBehaviour
    {
        public GameObject[] yourHand;
        public GameObject[] enemysHand;

        private void Start()
        {
            Manager.AddListenerOnChangeState(state =>
            {
                switch (state)
                {
                    case State.State.Janken:
                        this.gameObject.SetActive(true);
                        SetMyHand(0);
                        SetEnemyHand(0);
                        break;
                    case State.State.Result:
                        this.gameObject.SetActive(false);
                        break;
                }

            });

            this.gameObject.SetActive(false);
        }

        public void SetMyHand(int i) {
            foreach (GameObject h in yourHand)
            {
                h.SetActive(false);
            }
            yourHand[i].SetActive(true);
        }
        public void SetEnemyHand(int i)
        {
            foreach (GameObject h in enemysHand)
            {
                h.SetActive(false);
            }
            enemysHand[i].SetActive(true);
        }

    }
}
