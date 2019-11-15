using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Battle.State
{
    public class ReadyState: IState
    {
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
            Manager.NextTo(State.Janken);
        }

        public void MasterGetReady()
        {
            masterIsReady = true;
        }
        public void ClientGetReady()
        {
            clientIsReady = true;
        }
    }
}
