using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
namespace TKJP.Player
{
    public class TKJPPlayer : MonoBehaviour
    {
        private readonly HpHolder Hp = new HpHolder(100);
        private void Awake()
        {
            SceneContainer.BindConextObj(new HpAgent(Hp));
            SceneContainer.BindConextObj(this);
        }
        public IReadOnlyHpHolder GetHp()
        {
            return Hp;
        }
    }
}