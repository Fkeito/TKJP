﻿using UnityEngine;
using TKJP.Player;
using TKJP.Feature.Attack;
namespace TKJP.Battle.GrabbableObjects.Attack
{
    public class Sword : AttackGrabbable
    {
        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            AddAttacker<SwordAttacker>()
                .SetProperty(5,new AttackState(20));
        }public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity,angularVelocity);
            RemoveComponent<SwordAttacker>();
        }
    }
}