using System;
using _Scripts.VFX;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Health
{
    [RequireComponent(typeof(BlinkVisualEffect), typeof(DamagePopUp))]
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _maxHp;
        [SerializeField] private float _health;

        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        private bool _dead;

        private BlinkVisualEffect _blinkVisualEffect;
        private GameObject _attacker;

        public float Health => _health;
        public float MaxHp => _maxHp;

        public GameObject Attacker => _attacker;

        private void Awake()
        {
           
            _blinkVisualEffect = GetComponent<BlinkVisualEffect>();
        }

        public void TakeDamage(float damage, GameObject attacker)
        {
            if (_dead) return;
            if (damage < 0) throw new ArgumentException("Damage is less than zero");
            _attacker = attacker;
            _health -= damage;

            if (_health <= 0)
            {
                _dead = true;
                _onDamage?.Invoke();
                _onDie?.Invoke();
                return;
            }

            _onDamage?.Invoke();
            OnDamage();
        }

        private void OnDamage()
        {
            _blinkVisualEffect.Blink();
        }
    }
}