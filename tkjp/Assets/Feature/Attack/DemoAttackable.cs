using UnityEngine;

namespace Assets.Feature.Attack
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoAttackable : MonoBehaviour, IAttackable
    {
        public AttackResult Attack(AttackState state)
        {
            if(state.RandomNaAtai % 2 == 0)
            {
                Debug.Log(state.Damage + "ダメージを食らう");
                return AttackResult.Success();
            }
            else
            {
                return AttackResult.Miss();
            }
        }
    }
}