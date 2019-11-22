using System;
using UnityEngine;
using UniRx;
using TKJP.Feature.Attack;
namespace TKJP.Player
{
    public abstract class Attacker : MonoBehaviour
    {
        protected abstract int AttackDelay { get; }
        protected abstract AttackState AttackState { get; }
        private bool IsDelay = false;
        private IDisposable AttackDisposable;
        protected IDisposable TimerDisposable;
        protected virtual void Init()
        {
            //念のため作ったけど、このコンポーネントはDefenceGrabbable同様の使い方なので
            //AddComponentで初期化されてRemoveで死ぬ
            //初期化の必要ないはず
            AttackDisposable?.Dispose();
            TimerDisposable?.Dispose();
            IsDelay = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (IsDelay) return;
            var attackable = other.GetComponent<IAttackable>();
            if (attackable == null) return;
            Attack(attackable);
        }

        protected virtual async void Attack(IAttackable attackable)
        {
            var result = await attackable.Attack(AttackState);
            CheckResult(result);
        }

        protected virtual void CheckResult(AttackResult result)
        {
            IsDelay = true;
            TimerDisposable =  Observable
                .Timer(TimeSpan.FromSeconds(AttackDelay))
                .Subscribe(_ => {
                    IsDelay = false;
                });
        }

        private void OnDestroy()
        {
            Init();
        }
    }
}