using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TKJP.Battle.State;
using Photon;
using Photon.Pun;
using Photon.Realtime;
namespace TKJP.Battle.Game
{
    public class JankenManager : MonoBehaviour, IState
    {
        private BattleManager battle;

        public Battle.Game.JankenHand jankenhand;

        private JankenHand masterHand = JankenHand.None;
        private JankenHand clientHand = JankenHand.None;

        private bool isDraw;

        private PhotonView _photonview;

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

            _photonview = GetComponent<PhotonView>();
            if(_photonview == null)
            {
                _photonview = gameObject.AddComponent<PhotonView>();
            }
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
            if (PhotonNetwork.IsMasterClient)
            {
                _photonview.RPC("ClientNextTo", RpcTarget.Others);
            }
        }
        [PunRPC]
        public void ClientNextTo()
        {
            Manager.GetState<BattleManager>().rivalHand = clientHand;
            NextTo();
        } 

        public void SetJankenHand(int i)
        {
            jankenhand.SetMyHand(i+1);
            if (PhotonNetwork.IsMasterClient)
            {
                masterHand = (JankenHand)i;
                _photonview.RPC("SetMasterJankenHand", RpcTarget.MasterClient, i);
                Debug.Log("master hand is " + i);


                if (Common.Settings.device == Common.Settings.Device.Editor)
                {
                    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                    {
                        SetClientJankenHand(0);
                    }
                }
            }
            else
            {
                _photonview.RPC("SetClientJankenHand",RpcTarget.MasterClient,i);
            }
        }

        [PunRPC]
        public void SetClientJankenHand(int i)
        {
            clientHand = (JankenHand)i;

            Debug.Log(" hand is " + i);
        }
        public void SetMasterJankenHand(int i)
        {
            clientHand = (JankenHand)i;
        }
        public void Jadge()
        {
            StartCoroutine(_Jadge());
        }
        private IEnumerator _Jadge()
        {
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

