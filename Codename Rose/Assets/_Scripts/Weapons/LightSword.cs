using System.Collections;
using System.Collections.Generic;
using _Scripts.MapGeneration;
using _Scripts.PlayerScripts;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LightSword : Weapon
    {
        [SerializeField] private float _lightAttackDelayTime = 0.2f;
        [SerializeField] private float _heavyAttackDelayTime = 0.5f;
        [SerializeField] private AnimationClip _lightAttackClip;
        [SerializeField] private AnimationClip _heavyAttackClip;

        private List<Attack> _enqueuedAttacks = new();

        private WaitForSeconds _lightAttackDelay;
        private WaitForSeconds _heavyAttackDelay;

        private Vector2 _attackDirection;
        private Vector2 _initialSwordPosition;
        private Vector2 _mousePosition;

        private Timer _stateSwitchTimer;
        private Timer _comboTimer;
        private Camera _camera;

        private SpriteRenderer _spriteRenderer;
        private Transform _followTargetPosition;
        private Hero _player;


        private bool _isAttacking;
        private bool _canAttack;
        private bool _canEnqueue = true;


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
                StartCoroutine(EnqueueAttack(attackType));
                return;
            }

            _isAttacking = true;
            _isInCombat = true;
            _stateSwitchTimer.StartTimer();
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

        private IEnumerator EnqueueAttack(AttackTypes attackType)
        {
            if (_enqueuedAttacks.Count >= 2)
            {
                _canEnqueue = false;
            }

            if (!_canEnqueue) yield break;
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var attack = new Attack(attackType, mousePosition);
            _enqueuedAttacks.Add(attack);

            yield return new WaitUntil(() => !_isAttacking);

            Attack(_enqueuedAttacks[0].AttackType, _enqueuedAttacks[0].Position);
            _enqueuedAttacks.Remove(_enqueuedAttacks[0]);
            if (!_canEnqueue && _enqueuedAttacks.Count == 0) _canEnqueue = true;
        }

        private IEnumerator LightAttack(Vector2 mousePosition)
        {
            var initialPosition = transform.position;
            _weaponAnimator.Play("lightAttack");
            yield return _lightAttackDelay;

            var coroutine = StartCoroutine(GoToLocation(mousePosition));
            yield return coroutine;
            LightAttackHitCheck(initialPosition);
            _isAttacking = false;
        }

        private IEnumerator HeavyAttack(Vector2 mousePosition)
        {
            var initialPosition = transform.position;
            yield return _lightAttackDelay;
            var coroutine = StartCoroutine(GoToLocation(mousePosition));
            yield return coroutine;
            _weaponAnimator.Play("heavyAttack");
            yield return _heavyAttackDelay;
            HeavyAttackHitCheck(initialPosition);
            yield return new WaitForSeconds(_heavyAttackClip.length - _heavyAttackDelayTime);
            _isAttacking = false;
        }

        private IEnumerator GoToLocation(Vector2 position)
        {
            while ((Vector2)transform.position != position)
            {
                Transform swordTransform;
                (swordTransform = transform).position =
                    Vector2.MoveTowards(transform.position, position, _attackSpeed);
                var rotation = position - (Vector2)swordTransform.position;
                var zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, zRotation);
                yield return null;
            }
        }

        private void LightAttackHitCheck(Vector2 initialPosition)
        {
            var hits = new RaycastHit2D[10];
            var numHits = Physics2D.LinecastNonAlloc(transform.position, initialPosition, hits, _layerToHit);

            for (int i = 0; i < numHits; i++)
            {
                _swordModifyHealthComponent.ModifyHealth(hits[i].collider.gameObject, _player.gameObject,
                    initialPosition, transform.position, AttackTypes.Light);
            }
        }

        private void HeavyAttackHitCheck(Vector2 initialPosition)
        {
            var hits = new RaycastHit2D[10];
            var radius = 1.5f;
            var numHits = Physics2D.CircleCastNonAlloc(transform.position, radius, Vector2.zero, hits, radius,
                _layerToHit);

            for (int i = 0; i < numHits; i++)
            {
                _swordModifyHealthComponent.ModifyHealth(hits[i].collider.gameObject, _player.gameObject,
                    initialPosition, transform.position, AttackTypes.Heavy);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(transform.position, 1);
        }


        private void RotateToCursor()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var rotation = mousePosition - transform.position;
            var zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, zRotation);
        }

        private void FollowPlayer()
        {
            transform.position = Vector2.Lerp(transform.position, _followTargetPosition.position + Vector3.left / 2,
                _followSpeed * Time.deltaTime);
        }

        private void UpdateIdleDirection()
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}