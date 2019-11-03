using TKJP.Player;
using TKJP.Feature.Attack;
using UniRx.Async;
namespace TKJP.Battle.GrabbableObjects.Defence
{
    public class HelmetAttackable : DefenceAttackable
    {
        public override async UniTask<AttackResult> Attack(AttackState state)
        {
            await UniTask.CompletedTask;
            AttackAgent(state.Damage / 2);
            return AttackResult.Success();
        }
    }
}