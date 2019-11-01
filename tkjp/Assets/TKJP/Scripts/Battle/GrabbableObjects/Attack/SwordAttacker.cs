using UnityEngine;
using TKJP.Player;
using TKJP.Feature.Attack;
namespace TKJP.Battle.GrabbableObjects.Attack
{
    public class SwordAttacker : Attacker
    {
        protected override int AttackDelay { get => attackDelay; } 
        private int attackDelay = 10;

        protected override AttackState AttackState => attackState;
        private AttackState attackState = new AttackState(10);

        public void SetProperty(int delay,AttackState state)
        {
            attackDelay = delay;
            attackState = state;
        }

        protected override void CheckResult(AttackResult result)
        {
            base.CheckResult(result);
            Debug.Log((result.Result == Feature.Result.ResultEnum.Success) ?"攻撃成功" :"攻撃失敗");
        }
    }
}