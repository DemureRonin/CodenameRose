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
        [SerializeField] private Transform _projectilePosition;
        private Transform _target;
        private Coroutine _coroutine;
        private WaitForSeconds _attackDelay;
        private bool _isAgro;
        private bool _suddenDamage;
        private bool _attacking;
        private static readonly int Attack = Animator.StringToHash("projectileAttack");
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private AnimationClip _projectileAttackClip;

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
            StartCoroutine(AttackChance());
        }

        public void OnSuddenDamage()
        {
            if (_isAgro) return;
            _isAgro = true;
            _target = _healthComponent.Attacker.transform;

            _vision.SetActive(false);
            StartState(MoveToTarget());
            StartCoroutine(AttackChance());
        }

        private IEnumerator AttackChance()
        {
            while (!_attacking)
            {
                var rand = Random.value;
                if (rand > 0.8f)
                {
                    StartState(ProjectileAttack());
                }

                yield return new WaitForSeconds(2f);
            }
        }

        public void SpawnProjectile()
        {
           
        }

        private IEnumerator ProjectileAttack()
        {
            _attacking = true;
            _animator.SetTrigger(Attack);
            _movementVector = Vector2.zero;
            yield return new WaitForSeconds(_projectileAttackClip.length);
            Instantiate(_projectilePrefab, _projectilePosition.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            _attacking = false;
            StartState(MoveToTarget());
            StartCoroutine(AttackChance());
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