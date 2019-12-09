using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Player;
using TKJP.Battle.State;
using TKJP.Feature.Hp;
using TKJP.Common.Container;
namespace TKJP.Battle.Game
{
    public class BattleManager : MonoBehaviour, IState
    {
        private ResultManager result;

        private TKJPGrabber.GrabType grabType;

        public GameObject[] weapons;
        private WeaponManager weaponManager;

        public TimeEvent onTimeChanged;
        public Timebar bar;

        private bool isSettled;
        private float battleTime;
        private float time;

        public void Initialize()
        {
            onTimeChanged = new TimeEvent();
            onTimeChanged.AddListener((time) => bar.onTimeChanged(time));

            result = Manager.GetState<ResultManager>();
            
            weaponManager = WeaponManager.Singleton;
            weaponManager.SetWeaponInfo(weapons);
        }

        public void OnChanged()
        {
            isSettled = false;
            battleTime = 5f;
            time = 0f;
            time = 0f;
        }

        public void OnUpdate()
        {
            time += Time.deltaTime;
            onTimeChanged.Invoke(time / battleTime);
        }

        public bool IsFinish()
        {
            return time > battleTime || isSettled;
        }

        public void NextTo()
        {
            grabType = TKJPGrabber.GrabType.None;
            Manager.NextTo(isSettled ? State.State.Result : State.State.Janken);
        }

        private void ClientNextTo()
        {
            NextTo();
        }

        public void BeSettled()
        {
            isSettled = true;
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
            BeSettled();
        }
    }
}
