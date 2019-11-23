using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
using TKJP.UI;
using UniRx;
namespace TKJP.Player
{
    public class TKJPPlayer : MonoBehaviour
    {
        private readonly HpHolder Hp = new HpHolder(100);
        [SerializeField] private HPBar HPBar;
        private HpAgent MyHp;
        private void Awake()
        {
            MyHp = new HpAgent(Hp);
            SceneContainer.BindConextObj(MyHp);
            SceneContainer.BindConextObj(this);

            GetHp()
                .OnChangeHp
                .Subscribe(value =>
                {
                    HPBar.SetValue(value);
                })
                .AddTo(gameObject);
        }
        public IReadOnlyHpHolder GetHp()
        {
            return Hp;
        }
    }
}