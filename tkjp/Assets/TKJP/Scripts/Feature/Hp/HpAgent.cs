namespace TKJP.Feature.Hp
{
    public sealed class HpAgent
    {
        private IHpHolder HpHolder { get; }
        public HpAgent(IHpHolder hpHolder)
        {
            HpHolder = hpHolder;
        }

        public void AddDamege(int damage)
        {
            HpHolder.Hp -= damage;
        }
    }
}