using TKJP.Player;
using TKJP.Feature.Attack;
using UniRx.Async;

namespace TKJP.Battle.GrabbableObjects.Defence
{
    public class ShieldAttackable : DefenceAttackable
    {
        public override async UniTask<AttackResult> Attack(AttackState state)
        {
            await UniTask.CompletedTask;
            return AttackResult.Miss();
        }
    }
}