using System.Collections;
using _Scripts.MapGeneration;
using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private Animator _animator;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Weapon _equippedWeapon;
        private Vector2 _movementVector;
        private Vector2 _lookDirection;

        public Vector2 LookDirection => _lookDirection;

        public Vector2 MovementVector => _movementVector;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private float _radius = 1;
        private Collider2D[] _interactionResult;
        private bool _isDashing;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
                _rigidbody.velocity = new Vector2(_movementVector.x * _speed, _movementVector.y * _speed);
            Animate();
        }

        private void Update()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * MapGenerator.SortingDiscretion);
        }

        public void SetVector(Vector2 vector)
        {
            _movementVector = vector;
            if (vector != Vector2.zero)
            {
                _lookDirection = vector;
            }
        }

        private void Animate()
        {
            switch (_movementVector.y)
            {
                case > 0:
                    _animator.Play("back_idle");
                    break;
                case < 0:
                    _animator.Play("forward_idle");
                    break;
                default:
                {
                    if (_movementVector.x != 0)
                    {
                        _animator.Play("right_idle");
                        transform.localScale = new Vector3(_movementVector.x, 1, 1);
                    }

                    break;
                }
            }
        }

        public void CheckCollectableObjects()
        {
            var size = Physics2D.OverlapCircle(transform.position, _radius, _layer);
            if (size == null) return;
            var coll = size.gameObject.GetComponent<Collectable.Collectable>();
            coll.Collect();
        }

        public void Attack(AttackTypes attackType)
        {
            _equippedWeapon.Attack(attackType);
        }

        public IEnumerator Dash()
        {
            _isDashing = true;
            _rigidbody.AddForce(_lookDirection * _dashSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
            _rigidbody.velocity = Vector2.zero;
            _isDashing = false;

        }
    }
}