using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TKJP.Feature.Hp;
using TKJP.UI;
using Photon.Pun;

public class TKJPEnemy : MonoBehaviour
{
    private HpHolder hp = new HpHolder(100);
    [SerializeField] private HPBar HPBar;

    public PhotonView headView;
    public PhotonView rightView;
    public PhotonView leftView;

    public void Constructor(int headid, int rightid, int leftid)
    {
        headView.ViewID = headid;
        rightView.ViewID = rightid;
        leftView.ViewID = leftid;

        hp
        .OnChangeHp
        .Subscribe(value => {
                HPBar.SetValue(value);
        })
        .AddTo(gameObject);
    }

    public void SetHP(int hp)
    {
        this.hp.Hp = hp;
    }

}
