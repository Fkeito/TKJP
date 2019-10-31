using TKJP.Player;
using TKJP.Feature.Attack;
using TKJP.Feature.Hp;
using UniRx.Async;
namespace TKJP.Battle.GrabbableObjects.Defence
{
    public class HelmetAttackable : DefenceAttackable
    {
        public override async UniTask<AttackResult> Attack(AttackState state)
        {
            await UniTask.CompletedTask;
            HpAgent.AddDamege(state.Damage / 2);
            return AttackResult.Success();
        }
    }
}