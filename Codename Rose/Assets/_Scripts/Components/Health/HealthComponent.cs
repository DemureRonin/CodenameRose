using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        
        public void TakeDamage(float damage)
        {
            if (_health <= 0) return;
            if (damage < 0) throw new ArgumentException("Damage is less than zero");
            _health -= damage;

            if (_health <= 0)
            {
                _onDie?.Invoke();
                return;
            }

            _onDamage?.Invoke();
        }
    }
}