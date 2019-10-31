namespace TKJP.Feature.Hp
{
    public interface IHpHolder:IReadOnlyHpHolder
    {
        new int Hp { get;  set; }
    }
    public interface IReadOnlyHpHolder
    {
        int Hp { get; }
    }
}