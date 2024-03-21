using _Scripts.Components;
using UnityEngine;

namespace _Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Creature : MonoBehaviour
    {
        [SerializeField] protected float _movementSpeed;
        [SerializeField] protected Animator _animator;

        protected Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;

        protected Vector2 _movementVector;
        protected Vector2 _lookDirection;

        public Vector2 MovementVector => _movementVector;
        public Vector2 LookDirection => _lookDirection;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody.gravityScale = 0;
            _rigidbody.freezeRotation = true;
        }

        protected virtual void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_movementVector.x * _movementSpeed, _movementVector.y * _movementSpeed);
            //  Animate();
        }

        private void Update()
        {
            SortOrderInLayer();
        }

        private void SortOrderInLayer()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * RenderSorting.SortingDiscretion) + 3;
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
    }
}