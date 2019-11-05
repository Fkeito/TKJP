using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Async;
using TKJP.UI;
using TKJP.Feature.Hp;
using TKJP.Feature.Attack;

public class ScarecrowController : MonoBehaviour, IAttackable
{
    public HPBar HPBar;
    private int MaxHp = 100;
    private HpHolder HpHolder = new HpHolder(100);
    HpAgent agent;

    // Start is called before the first frame update
    void Start()
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
        agent = new HpAgent(holder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask<AttackResult> Attack(AttackState state)
    {
        await UniTask.CompletedTask;
        agent.AddDamege(state.Damage);
        return AttackResult.Success();
    }
}
