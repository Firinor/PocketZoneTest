using System;
using TMPro;
using UnityEngine;

namespace Observers
{
    public class BulletsObserver : MonoBehaviour, IObserver<int>
    {
        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private Player unit;

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw error;
        }

        public void OnNext(int value)
        {
            text.text = value.ToString();
        }

        private void OnEnable()
        {
            unit.Weapon.Bullets.Subscribe(this);
        }
    }
}