namespace TKJP.Player
{
    public abstract class DefenceGrabbable : TKJPGrabbable
    {
        private DefenceAttackable DefenceAttackable;
        protected void AddAttackable<T>() where T : DefenceAttackable {
            DefenceAttackable = gameObject.AddComponent<T>();
        }

        protected void RemoveAttackable()
        {
            Destroy(DefenceAttackable);
            DefenceAttackable = null;
        }
    }
}