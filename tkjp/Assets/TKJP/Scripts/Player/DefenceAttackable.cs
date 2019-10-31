using UnityEngine;
using TKJP.Feature.Attack;
using TKJP.Feature.Hp;
using TKJP.Common.Container;
using UniRx.Async;

namespace TKJP.Player
{
    public abstract class DefenceAttackable : MonoBehaviour,IAttackable
    {
        //Containerを使って自分を初期化する
        protected HpAgent HpAgent;
        void Awake()
        {
            Init();
        }
        public void Init()
        {
            HpAgent = SceneContainer.Instance.GetContextObj<HpAgent>();
        }

        public abstract UniTask<AttackResult> Attack(AttackState state);
    }
}