using UnityEngine;
using UnityEngine.UI;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
using UniRx;
using System;

namespace TKJP.Battle.GrabbableObjects.Defence.Test
{
    public class HelmetTester : MonoBehaviour,IHpHolder
    {
        
        public GameObject HelmetPrefab;

        public int Hp {
            get {
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

        private Helmet Helmet;
        void Start()
        {
            //このバインド作業をプレイヤーが自分でやる
            SceneContainer.Instance.Bind(new HpAgent(this));

            //これ以降に初期化されたDefenceAttackableはコンテナから値をとってくるのでこれが入る

            Helmet = Instantiate(HelmetPrefab,Vector3.up * 0.5f,Quaternion.identity).GetComponent<Helmet>();

            Debug.Log("ヘルメットつける");
            Helmet.PutOn();

            OnChangeHp.Subscribe(hp => Text.text = "HP:" + hp);


            Invoke("HelmetPutOff", 5.0f);
        }
        void HelmetPutOff()
        {
            Debug.Log("ヘルメット脱ぐ");
            Helmet.PutOff();
        }
    }
}