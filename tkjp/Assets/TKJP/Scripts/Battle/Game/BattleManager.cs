using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Player;
using TKJP.Battle.State;

namespace TKJP.Battle.Game
{
    public class BattleManager : MonoBehaviour
    {
        private BattleState state;
        private ResultManager result;
        private TKJPGrabber.GrabType grabType;

        void Start()
        {
            state = Manager.GetState<BattleState>();
            result = Manager.GetStateObj(State.State.Result).GetComponent<ResultManager>();
        }

        void OnEnable()
        {
            if (grabType == TKJPGrabber.GrabType.All) grabType = TKJPGrabber.GrabType.None;
        }

        void OnDisable()
        {
            grabType = TKJPGrabber.GrabType.None;
        }

        public void SetGrabType(int jankenResult)
        {
            switch (jankenResult)
            {
                case 2:
                    grabType = TKJPGrabber.GrabType.Attack;
                    break;
                case 1:
                    grabType = TKJPGrabber.GrabType.Defence;
                    break;
                default:
                    grabType = TKJPGrabber.GrabType.None;
                    break;
            }
        }

        public void BeSettled(Result result)
        {
            this.result?.SetResult(result);
            state?.BeSettled();
        }
    }
}
