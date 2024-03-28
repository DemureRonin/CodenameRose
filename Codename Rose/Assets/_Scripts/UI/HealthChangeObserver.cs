using System;
using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.UI
{
    public class HealthChangeObserver : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        public delegate void HealthEvent(float currentHp, float maxHp);

        public static event HealthEvent OnHealthChanged;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            
        }

        private void Start()
        {
            ObserveHealthChanged();
        }

        public void ObserveHealthChanged()
        {
            var maxHp = _healthComponent.MaxHp;
            var currentHp = _healthComponent.Health;
            OnHealthChanged?.Invoke(currentHp, maxHp);
        }
    }
}