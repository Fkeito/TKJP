using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;
using TKJP.Battle.State;
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

        public void Initialize()
        {
            masterIsReady = clientIsReady = false;
            _photonView = GetComponent<PhotonView>();
            if(_photonView == null)
            {
                _photonView = gameObject.AddComponent<PhotonView>();
            }
        }

        public void OnChanged()
        {
            Initialize();
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
            if (PhotonNetwork.IsMasterClient)
            {
                masterIsReady = true;
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
