using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Player;
using TKJP.Battle.State;
using TKJP.Feature.Hp;

namespace TKJP.Battle.Game
{
    public class BattleManager : MonoBehaviour, IState
    {
        private ResultManager result;
        private TKJPGrabber.GrabType grabType;

        public ScarecrowController/*PlayerController*/ clientPlayer;
        //public PlayerController masterPlayer;

        public GameObject[] weapons;
        private WeaponManager weaponManager;

        public TimeEvent onTimeChanged;

        private bool isSettled;
        private float battleTime;
        private float time;

        public void Initialize()
        {
            onTimeChanged = new TimeEvent();
            isSettled = false;
            battleTime = 5f;
            time = 0f;

            result = Manager.GetState<ResultManager>();

            weaponManager = WeaponManager.Singleton;
            weaponManager.SetWeaponInfo(weapons);

            clientPlayer.GetHp().OnChangeHp.Subscribe(value => { if (value <= 0) BeSettled(Result.Win); }).AddTo(gameObject);
            //masterPlayer.GetHp().OnChangeHp.Subscribe(value => { if (value <= 0) BeSettled(Result.Lose); }).AddTo(gameObject);
        }

        public void OnChanged()
        {
            time = 0f;
        }

        public void OnUpdate()
        {
            time += Time.deltaTime;
            onTimeChanged.Invoke(time);
        }

        public bool IsFinish()
        {
            return time > battleTime || isSettled;
        }

        public void NextTo()
        {
            Manager.NextTo(isSettled ? State.State.Result : State.State.Janken);
        }

        public void BeSettled()
        {
            isSettled = true;
        }

        void OnEnable()
        {
            if (grabType == TKJPGrabber.GrabType.All) grabType = TKJPGrabber.GrabType.None;
        }

        void OnDisable()
        {
            grabType = TKJPGrabber.GrabType.None;

            foreach (GameObject weapon in weapons)
            {
                weapon.SetActive(true);
                Trs resetTrs = weaponManager?.GetFirstTrs(weapon) ?? new Trs(weapon.transform);
                weapon.transform.position = resetTrs.position;
                weapon.transform.rotation = resetTrs.rotation;
            }
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
