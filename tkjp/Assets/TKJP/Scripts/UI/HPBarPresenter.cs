using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TKJP.Feature.Hp;
namespace TKJP.UI
{
    public class HPBarPresenter : MonoBehaviour
    {
        [SerializeField] private HPBar HPBar;
        private IReadOnlyHpHolder readOnlyHpHolder;
        private void Start()
        {
            readOnlyHpHolder = GetComponent<T>
        }
    }
}