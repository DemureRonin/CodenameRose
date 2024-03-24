using System;
using _Scripts.VFX;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Health
{
    [RequireComponent(typeof(BlinkVisualEffect), typeof(DamagePopUp))]
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private bool _immortal;
        private float _damage;

        private DamagePopUp _damagePopUp;
        private BlinkVisualEffect _blinkVisualEffect;
        private GameObject _attacker;

        public GameObject Attacker => _attacker;

        private void Awake()
        {
            _damagePopUp = GetComponent<DamagePopUp>();
            _blinkVisualEffect = GetComponent<BlinkVisualEffect>();
        }

        public void TakeDamage(float damage, GameObject attacker)
        {
            _damage = damage;
            if (_immortal)
            {
                _onDamage?.Invoke();
                OnDamage();
                return;
            }

            if (_health <= 0) return;
            if (damage < 0) throw new ArgumentException("Damage is less than zero");
            _attacker = attacker;
            _health -= damage;

            if (_health <= 0)
            {
                _onDie?.Invoke();
                return;
            }

            _onDamage?.Invoke();
            OnDamage();
        }

        private void OnDamage()
        {
            _damagePopUp.SpawnDamagePopUp(_damage);
            _blinkVisualEffect.Blink();
        }
    }
}