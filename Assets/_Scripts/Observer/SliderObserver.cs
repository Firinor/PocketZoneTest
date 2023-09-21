using System;
using UnityEngine;
using UnityEngine.UI;

namespace Observers
{
    public class SliderObserver : MonoBehaviour, IObserver<float>
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Unit unit;

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(float value)
        {
            slider.value = value;
        }

        private void OnEnable()
        {
            slider.maxValue = unit.MaxHealth;
            unit.CurrentHealth.Subscribe(this);
        }
    }
}