using System.Collections;
using _Scripts.Components;
using _Scripts.Components.Health;
using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Weapon _equippedWeapon;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _dashSpeed;
        private SpriteRenderer _spriteRenderer;

        private Vector2 _movementVector;
        private Vector2 _lookDirection;

        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private bool _isDashing;

        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();

            _rigidBody.gravityScale = 0;
            _rigidBody.freezeRotation = true;
        }

        private void Update()
        {
            if (_rigidBody.velocity != Vector2.zero)
                _lookDirection = _rigidBody.velocity;

            SortOrderInLayer();
        }

        protected void FixedUpdate()
        {
            if (_isDashing) return;
            _rigidBody.velocity = new Vector2(_movementVector.x * _movementSpeed, _movementVector.y * _movementSpeed);
            Animate();
        }

        public void SetVector(Vector2 vector)
        {
            _movementVector = vector;
            if (vector != Vector2.zero)
            {
                _lookDirection = vector;
            }
        }

        public void Attack(AttackTypes attackType) => _equippedWeapon.OnAttack(attackType);

        public IEnumerator Dash()
        {
            _isDashing = true;
            _rigidBody.AddForce(_lookDirection * _dashSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
            _rigidBody.velocity = Vector2.zero;
            _isDashing = false;
        }


        private void Animate()
        {
            _animator.SetFloat(XDirection, _lookDirection.x);
            _animator.SetFloat(YDirection, _lookDirection.y);
        }

        private void SortOrderInLayer()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * RenderSorting.SortingDiscretion) + 3;
        }
    }
}