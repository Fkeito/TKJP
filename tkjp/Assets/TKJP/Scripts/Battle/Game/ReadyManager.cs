using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;
using TKJP.Battle.State;
using TKJP.Player;
using Photon.Pun;
using Photon.Realtime;
namespace TKJP.Battle.Game
{
    public class ReadyManager : MonoBehaviour, IState
    {
        public Transition transition;
        private bool masterIsReady;
        private bool clientIsReady;
        private PhotonView _photonView;
        [SerializeField]
        public void Initialize()
        {
            masterIsReady = clientIsReady = false;
            _photonView = GetComponent<PhotonView>();
            if(_photonView == null)
            {
                _photonView = gameObject.AddComponent<PhotonView>();
            }
        }
        public GameObject playerPrefab;
        private GameObject TKJPPlayer;
        public void OnChanged()
        {
            Initialize();
            if (PhotonNetwork.IsMasterClient)
            {
                TKJPPlayer = Instantiate(playerPrefab, Vector3.back * 1.65f, Quaternion.identity);
            }
            else
            {
                TKJPPlayer = Instantiate(playerPrefab, Vector3.forward * 1.65f, Quaternion.Euler(Vector3.up * 180f));
                var stateUIRoot = GameObject.Find("States");
                stateUIRoot.transform.eulerAngles = stateUIRoot.transform.eulerAngles + Vector3.up * 180;
            }
            TKJPPlayer.GetComponent<TKJPPlayer>().ViewSyncro();
        }

        public void OnUpdate()
        {

        }

        public bool IsFinish()
        {
            return masterIsReady && clientIsReady;
        }

        public void NextTo()
        {
            Manager.NextTo(State.State.Janken);
            if (PhotonNetwork.IsMasterClient)
            {
                _photonView.RPC("ClientNextTo",RpcTarget.Others);
            }
        }
        [PunRPC]
        private void ClientNextTo()
        {
            NextTo();
        }

        public void GetReady()
        {
            Debug.Log("ready");
            if (PhotonNetwork.IsMasterClient)
            {
                StartCoroutine(DelayMethod(1f, () =>
                {
                    masterIsReady = true;
                }));

                if (TKJP.Common.Settings.device == Common.Settings.Device.Editor)
                {
                    if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
                    {
                        StartCoroutine(DelayMethod(1f, () =>
                        {
                            clientIsReady = true;
                        }));
                    }
                }
            }
            else
            {
                _photonView.RPC("ClientGetReady",RpcTarget.MasterClient);
            }
        }
        [PunRPC]
        public void ClientGetReady()
        {
            clientIsReady = true;
        }

        public void BackMenu()
        {
            transition.Load();
        }

        private IEnumerator DelayMethod(float delayTime, System.Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action();
        }
    }
}
