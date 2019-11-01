namespace TKJP.Player
{
    public abstract class AttackGrabbable : TKJPGrabbable
    {
        protected T AddAttacker<T>() where T : Attacker
        {
            return AddComponent<T>();
        }
    }
}