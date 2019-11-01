using UnityEngine;
using UnityEngine.UI;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
using UniRx;
using System;

namespace TKJP.Battle.GrabbableObjects.Defence.Test
{
    public class ShieldTester : MonoBehaviour, IHpHolder
    {

        public GameObject ShieldPrefab;

        public int Hp
        {
            get
            {
                return _hp;
            }
            set
            {
                HpChangeSubject.OnNext(value);
                _hp = value;
            }
        }
        private int _hp = 100;

        public IObservable<int> OnChangeHp => HpChangeSubject;

        private readonly Subject<int> HpChangeSubject = new Subject<int>();

        public Text Text;

        void Start()
        {
            SceneContainer.Instance.Bind(new HpAgent(this));
            var shield = Instantiate(ShieldPrefab, Vector3.up * 0.5f, Quaternion.identity).GetComponent<BigShield>();
            //shield.GrabBegin(,);ここにテスト用の値を入れたかったけど無理、ざんねん
            //掴まれてないので、アタッカブルがアタッチされず、アタックが呼ばれない
            //ちゃんと掴まれるとシールドにシールドアタッカブルがアタッチされ、Failを返すようになるはず
            OnChangeHp.Subscribe(hp => Text.text = "HP:" + hp);
        }
    }
}