using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP.Player
{
    public class TKJPGrabber : OVRGrabber
    {
        [System.Serializable]
        public enum GrabType { None, Attack, Diffence, All };//None:なんもつかめへん, Attack:武器だけ, Diffence:防具だけ, All:全部掴めるやで
        [SerializeField]
        private GrabType grabType = GrabType.None;

        protected override void Awake()
        {
# if UNITY_EDITOR
            m_anchorOffsetPosition = transform.localPosition;
            m_anchorOffsetRotation = transform.localRotation;

            // If we are being used with an OVRCameraRig, let it drive input updates, which may come from Update or FixedUpdate.

            OVRCameraRig rig = null;
            if (transform.parent != null && transform.parent.parent != null)
                rig = transform.parent.parent.GetComponent<OVRCameraRig>();

            if (rig != null)
            {
                operatingWithoutOVRCameraRig = false;
            }

            m_parentHeldObject = true;
#else
            base.Awake();
#endif
        }
        private void Update()
        {
            if(Common.Settings.device == Common.Settings.Device.Editor)
            {
                float prevFlex = m_prevFlex;
                // Update values from inputs
                m_prevFlex = Input.GetMouseButton(0) ? 1f : 0f;
                GrabOrRelease(prevFlex);
            }
        }
        private void GrabOrRelease(float prevFlex)
        {
            if ((m_prevFlex >= grabBegin) && (prevFlex < grabBegin))
            {
                GrabBegin();
            }
            else if ((m_prevFlex <= grabEnd) && (prevFlex > grabEnd))
            {
                GrabEnd();
            }
        }

        protected override void GrabBegin()
        {
            switch (grabType)
            {
                case GrabType.Attack:
                    Grab<AttackGrabbable>();
                    break;
                case GrabType.Diffence:
                    Grab<DifferenceGrabbable>();
                    break;
                case GrabType.All:
                    Grab<TKJPGrabbable>();
                    break;
            }
        }

        private void Grab<T>() where T: OVRGrabbable
        {
            float closestMagSq = float.MaxValue;
            T closestGrabbable = null;
            Collider closestGrabbableCollider = null;

            // Iterate grab candidates and find the closest grabbable candidate
            foreach (OVRGrabbable grabbable in m_grabCandidates.Keys)
            {
                T tGrabbable = grabbable.gameObject.GetComponent<T>();
                if (!tGrabbable) continue;

                bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab);
                if (!canGrab)
                {
                    continue;
                }

                for (int j = 0; j < grabbable.grabPoints.Length; ++j)
                {
                    Collider grabbableCollider = grabbable.grabPoints[j];
                    // Store the closest grabbable
                    Vector3 closestPointOnBounds = grabbableCollider.ClosestPointOnBounds(m_gripTransform.position);
                    float grabbableMagSq = (m_gripTransform.position - closestPointOnBounds).sqrMagnitude;
                    if (grabbableMagSq < closestMagSq)
                    {
                        closestMagSq = grabbableMagSq;
                        closestGrabbable = tGrabbable;
                        closestGrabbableCollider = grabbableCollider;
                    }
                }
            }

            // Disable grab volumes to prevent overlaps
            GrabVolumeEnable(false);

            //Todo: この先のthis.transformをm_grippedTransformに変えろ
            if (closestGrabbable != null)
            {
                if (closestGrabbable.isGrabbed)
                {
                    //closestGrabbable.grabbedBy.OffhandGrabbed(closestGrabbable);
                    Debug.Log(closestGrabbable.name + " isGrabbed");
                    return;
                }

                m_grabbedObj = closestGrabbable;
                m_grabbedObj.GrabBegin(this, closestGrabbableCollider);

                m_lastPos = transform.position;
                m_lastRot = transform.rotation;

                // Set up offsets for grabbed object desired position relative to hand.
                if (m_grabbedObj.snapPosition)
                {
                    m_grabbedObjectPosOff = m_gripTransform.localPosition;
                    if (m_grabbedObj.snapOffset)
                    {
                        Vector3 snapOffset = m_grabbedObj.snapOffset.position;
                        if (m_controller == OVRInput.Controller.LTouch) snapOffset.x = -snapOffset.x;
                        m_grabbedObjectPosOff += snapOffset;
                    }
                }
                else
                {
                    Vector3 relPos = m_grabbedObj.transform.position - transform.position;
                    relPos = Quaternion.Inverse(transform.rotation) * relPos;
                    m_grabbedObjectPosOff = relPos;
                }

                if (m_grabbedObj.snapOrientation)
                {
                    m_grabbedObjectRotOff = m_gripTransform.localRotation;
                    if (m_grabbedObj.snapOffset)
                    {
                        m_grabbedObjectRotOff = m_grabbedObj.snapOffset.rotation * m_grabbedObjectRotOff;
                    }
                }
                else
                {
                    Quaternion relOri = Quaternion.Inverse(transform.rotation) * m_grabbedObj.transform.rotation;
                    m_grabbedObjectRotOff = relOri;
                }

                // Note: force teleport on grab, to avoid high-speed travel to dest which hits a lot of other objects at high
                // speed and sends them flying. The grabbed object may still teleport inside of other objects, but fixing that
                // is beyond the scope of this demo.
                MoveGrabbedObject(m_lastPos, m_lastRot, true);
                if (m_parentHeldObject)
                {
                    m_grabbedObj.transform.parent = m_gripTransform.transform;
                }
            }
        }
    }
}
