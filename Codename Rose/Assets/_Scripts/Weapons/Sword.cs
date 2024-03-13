using System.Collections;
using _Scripts.MapGeneration;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Sword : Weapon
    {
        [SerializeField] private float _followSpeed;
        [SerializeField] private Animator _weaponAnimator;
        [SerializeField] private float _idleTimeToSwitchState;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackSpeed;

        private Vector2 _attackDirection;
        private Vector2 _lastAttackPosition;

        private Timer _stateSwitchTimer;
        private Camera _camera;

        private SpriteRenderer _spriteRenderer;
        private Transform _followTargetPosition;
        private Hero _player;

        private bool _isAttacking;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _stateSwitchTimer = new Timer();
            _player = FindAnyObjectByType<Hero>();
            _followTargetPosition = _player.transform;
            _stateSwitchTimer.Value = _idleTimeToSwitchState;
        }

        private void Update()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * MapGenerator.SortingDiscretion) - 1;
            if (_stateSwitchTimer.IsReady)
            {
                _isAttacking = false;
                _lastAttackPosition = _player.transform.position;
            }

            if (!_isAttacking)
            {
                UpdateIdleDirection();
                FollowPlayer();
            }
            else
                RotateToCursor();
        }

        public override void Attack()
        {
            _isAttacking = true;
            _stateSwitchTimer.StartTimer();
            transform.position = Vector2.Lerp(transform.position, _lastAttackPosition, 90);
            StartCoroutine(LightAttack());
        }

        private IEnumerator LightAttack()
        {
            _weaponAnimator.Play("attack");
            yield return new WaitForSeconds(0.2f);

            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var swordPosition = transform.position;
            var attackDirection = new Vector2(mousePosition.x - swordPosition.x, mousePosition.y - swordPosition.y)
                .normalized;
            var maxAttackPoint = new Vector2(attackDirection.x * _attackRange + swordPosition.x,
                attackDirection.y * _attackRange + swordPosition.y);

            _lastAttackPosition = transform.position = Vector2.Lerp(swordPosition, maxAttackPoint, _attackSpeed);
        }

        private void RotateToCursor()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var rotation = mousePosition - transform.position;
            var zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        public override void FollowPlayer()
        {
            transform.position = Vector2.Lerp(transform.position, _followTargetPosition.position + Vector3.left / 2,
                _followSpeed);
        }

        private void UpdateIdleDirection()
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}