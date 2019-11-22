using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
namespace TKJP.Player
{
    public class TKJPPlayer : MonoBehaviour
    {
        private HpHolder Hp;
        private void Awake()
        {
            SceneContainer.Instance.Bind(new HpAgent(Hp));
        }
        public IReadOnlyHpHolder GetHp()
        {
            return Hp;
        }
    }
}