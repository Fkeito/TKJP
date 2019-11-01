using TKJP.Player;
using UnityEngine;

namespace TKJP.Battle.GrabbableObjects.Defence
{
    public class BigShield : DefenceGrabbable
    {
        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            AddComponent<ShieldAttackable>();
        }
        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            RemoveComponent<ShieldAttackable>();
        }
    }
}