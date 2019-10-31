using TKJP.Feature.Attack;
using TKJP.Feature.Hp;
using UniRx.Async;
namespace TKJP.Player
{
    public class DefenceGrabbable : TKJPGrabbable, IAttackable
    {
        protected HpAgent HpAgent;
        public virtual async UniTask<AttackResult> Attack(AttackState state)
        {
            await UniTask.CompletedTask;
            return AttackResult.Miss();
        }
    }
}