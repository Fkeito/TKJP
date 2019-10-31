using UnityEngine;
using TKJP.Common.Container;
using TKJP.Feature.Hp;
namespace TKJP.Battle.GrabbableObjects.Defence.Test
{
    public class HelmetTester : MonoBehaviour,IHpHolder,IReadOnlyHpHolder
    {
        public int Hp { get; set; } = 100;
        public GameObject HelmetPrefab;

        void Start()
        {
            SceneContainer.Instance.Bind(new HpAgent(this));
            var helmet = Instantiate(HelmetPrefab,Vector3.up * 0.5f,Quaternion.identity).GetComponent<Helmet>();
            helmet.PutOn();
        }
    }
}