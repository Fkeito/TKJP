using UnityEngine;
using UniRx.Async;
using UniRx.Async.Triggers;
using System.Threading;
using Assets.Feature.Attack;
namespace Assets.Feature.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoWeapon : AsyncCollisionTrigger
    {
        private Vector3 StartPos;
        private CancellationTokenSource TokenSource;
        private void Awake()
        {
            StartPos = transform.position;
        }
        private void Start()
        {
            TokenSource = new CancellationTokenSource();
            CollisionLoop(TokenSource.Token);
        }
        private void OnDestory()
        {
            TokenSource.Cancel();
            Debug.Log("停止時にエラー出るけど気にしないで、治し方わかんない");
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
            var result = await attackable.Attack(new AttackState(damage: Random.Range(0, 10)));
            Debug.Log("この攻撃は" + result.Result);
        }

        public void PosReset()
        {
            transform.position = StartPos;
        }
    }
}