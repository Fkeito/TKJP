using UnityEngine;
using TKJP.Battle.Game;

namespace TKJP.Player
{
    public abstract class DefenceGrabbable : TKJPGrabbable
    {
        private WeaponManager weaponManager;

        protected override void Start()
        {
            base.Start();
            weaponManager = WeaponManager.Singleton;
        }

        protected void AddAttackable<T>() where T : DefenceAttackable
        {
            AddComponent<T>();
        }

        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            //weaponManager.DeleteExcept(this.gameObject, TKJPGrabber.GrabType.Defence);
        }
    }
}