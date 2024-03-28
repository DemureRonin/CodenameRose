using System;
using _Scripts.VFX;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Health
{
    [RequireComponent(typeof(BlinkVisualEffect), typeof(DamagePopUp))]
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private bool _immortal;
        [SerializeField] private float _health;


        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        
        private float _incomingDamage;
        private float _maxHp;

        private DamagePopUp _damagePopUp;
        private BlinkVisualEffect _blinkVisualEffect;
        private GameObject _attacker;

        public float Health => _health;
        public float MaxHp => _maxHp;

        public GameObject Attacker => _attacker;

        private void Awake()
        {
           
            _damagePopUp = GetComponent<DamagePopUp>();
            _blinkVisualEffect = GetComponent<BlinkVisualEffect>();
        }

        private void Start()
        {
           
        }

        public void TakeDamage(float damage, GameObject attacker)
        {
            _incomingDamage = damage;
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
            _damagePopUp.SpawnDamagePopUp(_incomingDamage);
            _blinkVisualEffect.Blink();
        }

        public void SetHealth(float health)
        {
            if (health <= 0) throw new ArgumentException("Health passed is less or equal 0 at " + gameObject.name);
            _health = health;
            _maxHp = _health;
        }
    }
}