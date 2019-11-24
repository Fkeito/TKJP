using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TKJP.Player;

namespace TKJP.Battle.Game {
    public class WeaponManager
    {
        private static WeaponManager _Singleton;
        public static WeaponManager Singleton
        {
            get {
                if (_Singleton == null) _Singleton = new WeaponManager();
                return _Singleton ; 
            }
            set
            {
                if (_Singleton == null) _Singleton = value;
            }
        }
        private Dictionary<GameObject, WeaponInfo> weaponInfos = new Dictionary<GameObject, WeaponInfo>();

        private TKJPGrabber right, left;

        public void SetWeaponInfo(GameObject[] weapons)
        {
            //Todo: random化
            weaponInfos = weapons.ToDictionary(
                                    w => w , 
                                    w => new WeaponInfo(
                                        w.transform.position,
                                        w.transform.rotation,
                                        w.GetComponent<AttackGrabbable>() ? TKJPGrabber.GrabType.Attack : w.GetComponent<DefenceGrabbable>() ? TKJPGrabber.GrabType.Defence : TKJPGrabber.GrabType.None,
                                        w.GetComponent<IDelatable>()
            ));
        }

        public Trs GetFirstTrs(GameObject weapon)
        {
            return weaponInfos[weapon].transform;
        }

        public void ForceRelease()
        {
            right.Release();
            left.Release();
        }

        public void SetHand(TKJPGrabber right, TKJPGrabber left)
        {
            this.right = right;
            this.left = left;
        }

        //public void DeleteExcept(GameObject selectedWeapon, TKJPGrabber.GrabType selectedType)
        //{
        //    weaponInfos
        //        .Where(w => !w.Key.Equals(selectedWeapon))
        //        .Where(w => w.Value.type == selectedType)
        //        .ToList()
        //        .ForEach(w => w.Value.deletable.Delete());
        //}
    }

    public struct Trs
    {
        public Trs(Vector3 pos, Quaternion rot)
        {
            this.position = pos;
            this.rotation = rot;
        }
        public Trs(Transform trs)
        {
            this.position = trs.position;
            this.rotation = trs.rotation;
        }
        public Vector3 position;
        public Quaternion rotation;
    }
    public struct WeaponInfo
    {
        public WeaponInfo(Vector3 pos, Quaternion rot, TKJPGrabber.GrabType type, IDelatable del)
        {
            this.transform.position = pos;
            this.transform.rotation = rot;
            this.type = type;
            this.deletable = del;
        }
        public WeaponInfo(Transform trs, TKJPGrabber.GrabType type, IDelatable del)
        {
            this.transform.position = trs.position;
            this.transform.rotation = trs.rotation;
            this.type = type;
            this.deletable = del;
        }

        public Trs transform;
        public Player.TKJPGrabber.GrabType type;
        public IDelatable deletable;
    }
}
