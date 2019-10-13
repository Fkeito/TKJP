using UnityEngine;
using Assets.Feature.Result;
using Assets.Feature.Attack;
namespace Assets.Feature.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class DemoWeapon : MonoBehaviour
    {
        private Vector3 StartPos;
        private void Awake()
        {
            StartPos = transform.position;
        }
        public void OnCollisionEnter(Collision collision)
        {
            collision
                .collider
                .GetComponent<IAttackable>()
                ?.Attack(new AttackState(damage: Random.Range(0, 10), atai: Random.Range(0, 10)))
                .CallResult(result => {
                    Debug.Log("この攻撃は" + result);
                    PosReset();
                });
        }
        public void PosReset()
        {
            transform.position = StartPos;
        }
    }
}