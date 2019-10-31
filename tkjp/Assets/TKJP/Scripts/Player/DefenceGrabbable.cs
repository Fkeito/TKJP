using TKJP.Feature.Attack;
using UniRx.Async;
namespace TKJP.Player
{
    public class DefenceGrabbable : TKJPGrabbable, IAttackable
    {
        public virtual async UniTask<AttackResult> Attack(AttackState state)
        {
            await UniTask.CompletedTask;
            return AttackResult.Miss();
        }
    }
}