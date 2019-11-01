using System;
namespace TKJP.Feature.Hp
{
    public interface IHpHolder:IReadOnlyHpHolder
    {
        int Hp { get;  set; }
    }
    public interface IReadOnlyHpHolder
    {
        IObservable<int> OnChangeHp { get; }
    }
}