using System;
using UnityEngine;

namespace _Scripts.Utils
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private float _value;

        private float _stopTime;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public void StartTimer()
        {
            _stopTime = Time.time + _value;
        }

        public float TimeLasts => Mathf.Max(_stopTime - Time.deltaTime, 0);

        public bool IsReady => _stopTime <= Time.time;
    }
}