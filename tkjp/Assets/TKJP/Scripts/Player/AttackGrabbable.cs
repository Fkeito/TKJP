using UnityEngine;
using TKJP.Battle.Game;

namespace TKJP.Player
{
    public abstract class AttackGrabbable : TKJPGrabbable
    {
        private WeaponManager weaponManager;

        protected override void Start()
        {
            base.Start();
            weaponManager = WeaponManager.Singleton;
        }

        protected T AddAttacker<T>() where T : Attacker
        {
            return AddComponent<T>();
        }

        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            weaponManager.DeleteExcept(this.gameObject, TKJPGrabber.GrabType.Attack);
        }
    }
}