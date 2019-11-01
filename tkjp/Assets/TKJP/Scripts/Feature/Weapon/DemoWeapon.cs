using UnityEngine;
using UniRx.Async;
using UniRx.Async.Triggers;
using System.Threading;
using TKJP.Feature.Attack;
namespace TKJP.Feature.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoWeapon : AsyncCollisionTrigger
    {

        //こいつはasync/awaitの練習のために書いたやつで、ゲームの設計とかに全く関連しない
        //読む価値無し
        //でも防具をエディタだけでテストするときには便利だった
        private Vector3 StartPos;
        private CancellationTokenSource TokenSource;
        public int Damage = 10;
        private void Awake()
        {
            StartPos = transform.position;
        }
        private void Start()
        {
            TokenSource = new CancellationTokenSource();
            CollisionLoop(TokenSource.Token);
        }
        private void OnDisable()
        {
            Debug.Log("停止時にOperationCanceledException出るけど気にしないで、治し方わかんないし、たぶん放置してもいい");
        }
        private void OnDestory()
        {
            TokenSource.Cancel();
        }

        async void CollisionLoop(CancellationToken token)
        {
            if (token.IsCancellationRequested) return;
            var collision = await OnCollisionEnterAsync(token);
            var attackable = collision.collider.GetComponent<IAttackable>();
            if(attackable != null)
            {
                await Attack(attackable);
            }
            PosReset();
            CollisionLoop(token);
        }

        async UniTask Attack(IAttackable attackable)
        {
            var result = await attackable.Attack(new AttackState(Damage));
            Debug.Log("この攻撃は" + result.Result);
        }

        public void PosReset()
        {
            transform.position = StartPos;
        }
    }
}