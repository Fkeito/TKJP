namespace TKJP.Player
{
    public abstract class DefenceGrabbable : TKJPGrabbable
    {
        protected void AddAttackable<T>() where T : DefenceAttackable
        {
            AddComponent<T>();
        }
    }
}