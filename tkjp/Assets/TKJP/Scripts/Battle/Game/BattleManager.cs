using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Player;
using TKJP.Battle.State;
using TKJP.Feature.Hp;
using Photon.Pun;
using TKJP.Common.Container;
namespace TKJP.Battle.Game
{
    public class BattleManager : MonoBehaviour, IState
    {
        private ResultManager result;
        private TKJPGrabber.GrabType grabType;

        private TKJPPlayer MyPlayer;
        public ScarecrowController DebugEnemy;
        public GameObject[] weapons;
        private WeaponManager weaponManager;

        public TimeEvent onTimeChanged;
        public Timebar bar;

        private bool isSettled;
        private float battleTime;
        private float time;

        private PhotonView _photonview;
        private IDisposable HpDisposable;
        public void Initialize()
        {
            onTimeChanged = new TimeEvent();
            onTimeChanged.AddListener((time) => bar.onTimeChanged(time));

            isSettled = false;
            battleTime = 5f;
            time = 0f;

            result = Manager.GetState<ResultManager>();

            weaponManager = WeaponManager.Singleton;
            weaponManager.SetWeaponInfo(weapons);

            //clientPlayer.GetHp().OnChangeHp.Subscribe(value => { if (value <= 0) BeSettled(Result.Win); }).AddTo(gameObject);
            //masterPlayer.GetHp().OnChangeHp.Subscribe(value => { if (value <= 0) BeSettled(Result.Lose); }).AddTo(gameObject);
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                DebugEnemy?.GetHp().OnChangeHp.Subscribe(value => { if (value <= 0) BeSettled(Result.Win); }).AddTo(gameObject);
            }

            MyPlayer = SceneContainer.ContextObj<TKJPPlayer>();
            HpDisposable?.Dispose();
            HpDisposable = MyPlayer
                .GetHp()
                .OnChangeHp
                .Subscribe(value => {
                    _photonview.RPC("SetEnemyHp", RpcTarget.Others, value);
                    if (value <= 0) BeSettled(Result.Lose);
                });
            _photonview = GetComponent<PhotonView>();
            if(_photonview == null)
            {
                _photonview = gameObject.AddComponent<PhotonView>();
            }
        }
        [PunRPC]
        private void SetEnemyHp(int enemyHp)
        {
            if (enemyHp <= 0) BeSettled(Result.Win);
        }

        public void OnChanged()
        {
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
            Manager.NextTo(isSettled ? State.State.Result : State.State.Janken);
            if (PhotonNetwork.IsMasterClient)
            {
                _photonview.RPC("ClientNextTo", RpcTarget.Others);
            }
        }
        [PunRPC]
        private void ClientNextTo()
        {
            NextTo();
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
        public void OnDestroy()
        {
            HpDisposable?.Dispose();
            HpDisposable = null;
        }
    }
}
