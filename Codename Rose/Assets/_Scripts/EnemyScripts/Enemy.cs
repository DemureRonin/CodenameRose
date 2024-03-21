using System.Collections;
using _Scripts.Components.Health;
using _Scripts.Creatures;
using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class Enemy : Creature
    {
        [SerializeField] private GameObject _vision;
        [SerializeField] private HealthComponent _healthComponent;
        private Transform _target;
        private Coroutine _coroutine;
        private WaitForSeconds _attackDelay;
        private bool _isAgro;
        private bool _suddenDamage;

        private void StartState(IEnumerator state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(state);
        }

        public void OnSeeEnemy(GameObject target)
        {
            if (_isAgro) return;
            _isAgro = true;
            _target = target.transform;

            StartCoroutine(MoveToTarget());
        }

        public void OnSuddenDamage()
        {
            if (_isAgro) return;
            _isAgro = true;
            _target = _healthComponent.Attacker.transform;

            _vision.SetActive(false);
            StartCoroutine(MoveToTarget());
        }

        private IEnumerator MoveToTarget()
        {
            while (_isAgro)
            {
                _movementVector = (_target.transform.position - transform.position).normalized;

                yield return null;
            }
        }
    }
}