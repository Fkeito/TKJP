using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Feature.Hp;
using UniRx;
using UniRx.Triggers;
using System;
namespace TKJP.UI.Test
{
    public class HPBarTest : MonoBehaviour
    {
        public HPBar HPBar;
        private int MaxHp = 100;
        private HpHolder HpHolder = new HpHolder(100);

        private void Start()
        {
            HPBar.SetMaxValue(MaxHp);

            IReadOnlyHpHolder hpHolder = HpHolder;

            hpHolder
                .OnChangeHp
                .Subscribe(value =>
                {
                    HPBar.SetValue(value);
                    Debug.Log(value);
                })
                .AddTo(gameObject);

            IHpHolder holder = HpHolder;
            HpAgent agent = new HpAgent(holder);

            Observable
                .Timer(TimeSpan.FromSeconds(0.1f), TimeSpan.FromSeconds(0.1f))
                .Take(10)
                .Subscribe(_ => agent.AddDamege(10),() => Debug.Log("complet"));
        }
    }
}