using System.Collections;
using _Scripts.MapGeneration;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Sword : Weapon
    {
        [SerializeField] private float _lightAttackDelayTime = 0.2f;
        [SerializeField] private float _heavyAttackDelayTime = 0.5f;

        private int _comboCounter;

        private WaitForSeconds _lightAttackDelay;
        private WaitForSeconds _heavyAttackDelay;

        private Vector2 _attackDirection;
        private Vector2 _attackPoint;
        private Vector2 _lastAttackPosition;
        private Vector2 _initialSwordPosition;
        private Vector2 _mousePosition;

        private Timer _stateSwitchTimer;
        private Timer _comboTimer;
        private Camera _camera;

        private SpriteRenderer _spriteRenderer;
        private Transform _followTargetPosition;
        private Hero _player;

        private bool _isInCombat;
        private bool _isAttacking;
        private bool _canDamage;
        private bool _canAttack;

        public bool IsInCombat => _isInCombat;

        private void Awake()
        {
            _lightAttackDelay = new WaitForSeconds(_lightAttackDelayTime);
            _heavyAttackDelay = new WaitForSeconds(_heavyAttackDelayTime);
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
                _isInCombat = false;
                _lastAttackPosition = _player.transform.position;
            }

            if (!_isInCombat)
            {
                UpdateIdleDirection();
                FollowPlayer();
            }

            if (!_isAttacking && _isInCombat)
                RotateToCursor();
        }

        public override void Attack(AttackTypes attackType, Vector2 mousePosition = default)
        {
            if (_isAttacking)
            {
                StartCoroutine(EnqueueComboAttack(attackType));
                return;
            }

            _isAttacking = true;
            _isInCombat = true;
            _stateSwitchTimer.StartTimer();
            transform.position = Vector2.MoveTowards(transform.position, _lastAttackPosition, 90);
            if (mousePosition == default)
                mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            switch (attackType)
            {
                case AttackTypes.Light:
                    StartCoroutine(LightAttack(mousePosition));
                    break;
                case AttackTypes.Heavy:
                    StartCoroutine(HeavyAttack(mousePosition));
                    break;
            }
        }

        private IEnumerator EnqueueComboAttack(AttackTypes attackType)
        {
            if (_comboCounter > 3) yield break;
            _comboCounter++;
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            yield return new WaitUntil(() => !_isAttacking);

            Attack(attackType, mousePosition);
            _comboCounter--;
        }

        public override IEnumerator LightAttack(Vector2 mousePosition)
        {
            _weaponAnimator.Play("lightAttack");
            yield return _lightAttackDelay;

            _initialSwordPosition = transform.position;
            var playerPosition = _player.transform.position;

            var attackX = Mathf.Clamp(mousePosition.x, playerPosition.x - _maxAttackDistance,
                playerPosition.x + _maxAttackDistance);

            var attackY = Mathf.Clamp(mousePosition.y, playerPosition.y - _maxAttackDistance,
                playerPosition.y + _maxAttackDistance);

            _attackPoint = new Vector2(attackX, attackY);

            StartCoroutine(GoToLocation());
            yield return new WaitUntil(() => _canDamage);
            CheckEnemiesHit();
        }

        private IEnumerator GoToLocation()
        {
            while ((Vector2)transform.position != _attackPoint)
            {
                Transform swordTransform;
                (swordTransform = transform).position =
                    Vector2.MoveTowards(transform.position, _attackPoint, _attackSpeed);
                var rotation = _attackPoint - (Vector2)swordTransform.position;
                var zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, zRotation);
                yield return null;
            }

            _lastAttackPosition = transform.position;
            _canDamage = true;
        }

        private void CheckEnemiesHit()
        {
            var hits = new RaycastHit2D[10];
            var numHits = Physics2D.LinecastNonAlloc(transform.position, _initialSwordPosition, hits, _layerToHit);

            for (int i = 0; i < numHits; i++)
            {
                _modifyHealthComponent.ModifyHealth(hits[i].collider.gameObject, _player.gameObject);
            }

            _canDamage = false;
            _isAttacking = false;
        }

        public override IEnumerator HeavyAttack(Vector2 mousePosition)
        {
            _weaponAnimator.Play("heavyAttack");
            yield return _heavyAttackDelay;
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