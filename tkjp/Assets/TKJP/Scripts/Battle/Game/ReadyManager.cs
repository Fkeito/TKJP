using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Scene;
using TKJP.Battle.State;
using TKJP.Player;
namespace TKJP.Battle.Game
{
    public class ReadyManager : MonoBehaviour, IState
    {
        public Transition transition;
        private bool masterIsReady;
        private bool clientIsReady;

        public void Initialize()
        {
            // No Implement
        }
        public void OnChanged()
        {
            masterIsReady = clientIsReady = false;
        }

        public void OnUpdate()
        {
            // No Implement
        }

        public bool IsFinish()
        {
            return masterIsReady && clientIsReady;
        }

        public void NextTo()
        {
            Manager.NextTo(State.State.Janken);
        }
        private void ClientNextTo()
        {
            NextTo();
        }

        public void GetReady()
        {
            masterIsReady = true;
        }
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
