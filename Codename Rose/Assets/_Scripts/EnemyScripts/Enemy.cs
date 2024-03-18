using System.Collections;
using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _attackDelayTime;
        [SerializeField] private float _sphere1AttackOffset;
        [SerializeField] private float _playerThreshold;
        [SerializeField] private GameObject _sphereProjectile;

        private Transform _player;
        private Coroutine _coroutine;
        private WaitForSeconds _attackDelay;
        private bool _isAgro;

        private void Awake()
        {
            _attackDelay = new WaitForSeconds(_attackDelayTime);
        }

        private void StartState(IEnumerator state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(state);
        }

        public void OnSeePlayer(GameObject player)
        {
            if (_isAgro) return;
            _isAgro = true;
            _player = player.transform;

            StartState(Attack());
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (_isAgro)
            {
                var position = transform.position;
                var distanceBetweenPlayer = Vector2.Distance(_player.position, position);
                Vector2 direction = distanceBetweenPlayer > _playerThreshold ? (_player.position - position).normalized : (position - _player.position).normalized;


                var destination = (Vector2)position + direction;
                while ((Vector2)transform.position != destination)
                {
                    transform.position = Vector2.MoveTowards(transform.position, destination,
                        _movementSpeed);
                    yield return null;
                }

                yield return new WaitForSeconds(Random.Range(2f,5f));
            }
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                var position = transform.position;
                Vector2[] attackPositions =
                {
                    position,
                    /*new(position.x + _sphere1AttackOffset, position.y),
                    new(position.x - _sphere1AttackOffset, position.y),*/
                };
                foreach (var projectilePosition in attackPositions)
                {
                    Instantiate(_sphereProjectile, projectilePosition, Quaternion.identity);
                }

                yield return _attackDelay;
            }
        }
    }
}