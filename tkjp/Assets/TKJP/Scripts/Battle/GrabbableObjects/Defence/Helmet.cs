using TKJP.Player;
namespace TKJP.Battle.GrabbableObjects.Defence
{
    public class Helmet : DefenceGrabbable
    {
        public void PutOn()
        {
            AddAttackable<HelmetAttackable>();
        }
        public void PutOff()
        {
            RemoveComponent<HelmetAttackable>();
        }
    }
}