using System;
using UnityEngine;
using UnityEngine.UI;
using TKJP.Feature.Hp;
using UniRx;
namespace TKJP.UI
{
    public class HPBar : MonoBehaviour
    {
        public Image bar;
        int maxValue;
        public void SetMaxValue(int maxValue)
        {
            this.maxValue = maxValue;
        }
        public void SetValue(int value)
        {
            bar.fillAmount = (float) value / maxValue;
        }
    }
}