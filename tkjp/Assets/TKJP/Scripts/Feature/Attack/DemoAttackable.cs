using UniRx.Async;
using UnityEngine;
using TKJP.Feature.Result;
namespace TKJP.Feature.Attack
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoAttackable : MonoBehaviour, IAttackable
    {
        private bool IsAttackerClient = false;
        private bool IsTate = false;
        void Awake()
        {
            IsAttackerClient = Random.Range(1,20) % 2 == 0;
            Debug.Log((IsAttackerClient ? "攻撃側" : "防御側") + "クライアントになりました。");
        }
        public async UniTask<AttackResult> Attack(AttackState state)
        {
            AttackResult result;
            if (IsAttackerClient)
            {
                result = await WaitResult();
            }
            else
            {
                result = JudgeResult();
            }
            if (result.IsSuucess())
            {
                GetDamage(state.Damage);
            }
            return result;
        }

        private async UniTask<AttackResult> WaitResult()
        {
            await UniTask.Delay(1000);
            return JudgeResult();
        }
        private AttackResult JudgeResult()
        {
            IsTate = Random.Range(1, 20) % 2 == 0;
            Debug.Log("これは盾" + (IsTate ? "です" : "ではありません"));
            if (IsTate)
            {
                return AttackResult.Miss();
            }
            else
            {
                return AttackResult.Success();
            }
        }
        private void GetDamage(int damage)
        {
            Debug.Log(damage + "ダメージ食らった");
        }
    }
}