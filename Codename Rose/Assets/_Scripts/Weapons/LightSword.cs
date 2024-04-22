using System.Collections;
using System.Collections.Generic;
using _Scripts.MapGeneration.Map;
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
        [SerializeField] private GameObject _groundScar;

        private readonly List<Attack> _combo = new();
        private readonly int _maxCombo = 4;
        private int _comboCounter = 0;

        private WaitForSeconds _lightAttackDelay;
        private WaitForSeconds _heavyAttackDelay;

        private Vector2 _attackDirection;
        private Vector2 _initialSwordPosition;
        private Vector2 _mousePosition;

        private readonly Timer _stateSwitchTimer = new();
        private Timer _comboTimer = new();
        private Camera _camera;

        private SpriteRenderer _spriteRenderer;
        private Transform _followTargetPosition;
        private Hero _player;


        private bool _isAttacking;
        private bool _canAttack;
        private bool _canCombo = true;


        private void Awake()
        {
            _lightAttackDelay = new WaitForSeconds(_lightAttackDelayTime);
            _heavyAttackDelay = new WaitForSeconds(_heavyAttackDelayTime);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void Start()
        {
            _player = FindAnyObjectByType<Hero>();
            _followTargetPosition = _player.transform;
            _stateSwitchTimer.Value = _idleTimeToSwitchState;
        }
        private void Update()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * MapDef.SortingDiscretion) - 1;
            if (_stateSwitchTimer.IsReady)
            {
                _isInCombat = false;
            }

            if (!_isInCombat)
            {
                UpdateIdleDirection();
                FollowPlayer();
                ResetCombo();
            }

            if (!_isAttacking && _isInCombat)
                RotateToCursor();
        }

        private void ResetCombo()
        {
            _comboCounter = 0;
            _canCombo = true;
        }

        public override void OnAttack(AttackTypes attackType)
        {
            if (!_canCombo) return;
            if (attackType == AttackTypes.Heavy && _comboCounter != _maxCombo - 1) return;
            _isInCombat = true;
            _stateSwitchTimer.StartTimer();
            _comboCounter++;

            StartCoroutine(AddComboAttack(attackType));
        }

        private IEnumerator AddComboAttack(AttackTypes attackType)
        {
            if (_comboCounter >= _maxCombo)
            {
                _canCombo = false;
            }
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var attack = new Attack(attackType, mousePosition);
            _combo.Add(attack);

            yield return new WaitUntil(() => !_isAttacking);

            Attack(_combo[0].AttackType, _combo[0].Position);
            _combo.Remove(attack);
        }

        private void Attack(AttackTypes attackType, Vector2 mousePosition = default)
        {
            _isAttacking = true;

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


        private IEnumerator LightAttack(Vector2 mousePosition)
        {
            _initialSwordPosition = transform.position;
            _weaponAnimator.Play("lightAttack");
            yield return _lightAttackDelay;

            var coroutine = StartCoroutine(GoToLocation(mousePosition));
            yield return coroutine;

            LightAttackHitCheck();
            var transform1 = transform;
            Instantiate(_groundScar, transform1.position, transform1.rotation);
            _stateSwitchTimer.StartTimer();
            _isAttacking = false;
        }

        private IEnumerator HeavyAttack(Vector2 mousePosition)
        {
            _initialSwordPosition = transform.position;
            yield return _lightAttackDelay;

            var coroutine = StartCoroutine(GoToLocation(mousePosition));
            yield return coroutine;

            _weaponAnimator.Play("heavyAttack");
            yield return _heavyAttackDelay;

            HeavyAttackHitCheck();
            yield return new WaitForSeconds(_heavyAttackClip.length - _heavyAttackDelayTime);
            _stateSwitchTimer.StartTimer();

            _isAttacking = false;
        }

        private IEnumerator GoToLocation(Vector2 position)
        {
            while ((Vector2)transform.position != position)
            {
                Transform swordTransform;
                (swordTransform = transform).position =
                    Vector2.MoveTowards(transform.position, position, _attackSpeed * Time.deltaTime);
                var rotation = position - (Vector2)swordTransform.position;
                var zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0, 0, zRotation);
                yield return null;
            }
        }

        private void LightAttackHitCheck()
        {
            var hits = new RaycastHit2D[10];
            var numHits = Physics2D.LinecastNonAlloc(transform.position, _initialSwordPosition, hits, _layerToHit);

            for (int i = 0; i < numHits; i++)
            {
                _swordModifyHealthComponent.ModifyHealth(hits[i].collider.gameObject, _player.gameObject,
                    _initialSwordPosition, transform.position, AttackTypes.Light);
            }
        }

        private void HeavyAttackHitCheck()
        {
            var hits = new RaycastHit2D[10];
            var radius = 1.5f;
            var numHits = Physics2D.CircleCastNonAlloc(transform.position, radius, Vector2.zero, hits, radius,
                _layerToHit);

            for (int i = 0; i < numHits; i++)
            {
                _swordModifyHealthComponent.ModifyHealth(hits[i].collider.gameObject, _player.gameObject,
                    _initialSwordPosition, transform.position, AttackTypes.Heavy);
            }
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