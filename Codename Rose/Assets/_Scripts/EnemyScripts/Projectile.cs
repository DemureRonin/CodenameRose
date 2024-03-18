using System.Numerics;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace _Scripts.EnemyScripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] private float _startCooldown;

        private Hero _target;
        private Vector2 _targetPosition;
        private Timer _startTimer;
        private Timer _lifeTimer;
        private void Awake()
        {
            _target = FindAnyObjectByType<Hero>();
            _targetPosition = _target.transform.position;
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
        }

        private void Update()
        {
            if (_lifeTimer.IsReady)
            {
                Destroy(gameObject);
            }
            if (!_startTimer.IsReady)
            {
                _targetPosition = _target.transform.position;
                return;
            }
            
            GoToTarget();
        }

        private void GoToTarget()
        {
            var projectilePosition = transform.position;
          
            transform.position = Vector2.MoveTowards( projectilePosition, _targetPosition, _bulletSpeed);
        }
    }
}