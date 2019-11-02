using System;
using UniRx;

namespace TKJP.Feature.Hp
{
    public class HpHolder : IHpHolder
    {
        public int Hp {
            get {
                return _hp;
            }
            set
            {
                HpChangeSubject?.OnNext(value);
                _hp = value;
            }
        }
        private int _hp;

        public IObservable<int> OnChangeHp => HpChangeSubject;

        private readonly BehaviorSubject<int> HpChangeSubject;

        public HpHolder(int hp)
        {
            _hp = hp;
            HpChangeSubject = new BehaviorSubject<int>(hp);
        }
    }
}