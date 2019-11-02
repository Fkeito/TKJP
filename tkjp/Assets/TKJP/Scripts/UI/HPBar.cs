using System;
using UnityEngine;
using UnityEngine.UI;
using TKJP.Feature.Hp;
using UniRx;
namespace TKJP.UI
{
    public class HPBar : MonoBehaviour
    {
        public Slider Slider;
        public void SetMaxValue(int maxValue)
        {
            Slider.maxValue = maxValue;
        }
        public void SetValue(int value)
        {
            Slider.value = value;
        }
    }
}