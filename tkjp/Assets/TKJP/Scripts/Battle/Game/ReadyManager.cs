using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class ReadyManager : MonoBehaviour, IState
    {
        public Transition transition;

        private bool masterIsReady;
        private bool clientIsReady;

        public void Initialize()
        {
            masterIsReady = clientIsReady = false;
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
        }

        public void MasterGetReady()
        {
            masterIsReady = true;
            Debug.Log("master is ready!");
        }
        public void ClientGetReady()
        {
            clientIsReady = true;
            Debug.Log("client is ready");
        }

        public void BackMenu()
        {
            transition.Load();
        }
    }
}
