using System.Numerics;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace _Scripts.EnemyScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] private float _startCooldown;

        private Rigidbody2D _rigidbody;
        private Hero _target;
        private Vector2 _targetPosition;
        private Timer _startTimer;
        private Timer _lifeTimer;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
            _rigidbody.freezeRotation = true;
            _target = FindAnyObjectByType<Hero>();
            _startTimer = new Timer
            {
                Value = _startCooldown
            };
            _lifeTimer = new Timer()
            {
                Value = _bulletLifeTime
            };
            _startTimer.StartTimer();
            _lifeTimer.StartTimer();
            _direction = (_target.transform.position - transform.position).normalized * _bulletSpeed;
        }

        private void FixedUpdate()
        {
            if (_lifeTimer.IsReady)
            {
                Destroy(gameObject);
            }


            GoToTarget();
        }

        private void GoToTarget()
        {
            _rigidbody.velocity = _direction;
        }
    }
}