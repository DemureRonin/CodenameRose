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
        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");

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

            Animate();
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
            _animator.SetFloat(XDirection, _rigidbody.velocity.x);
            _animator.SetFloat(YDirection, _rigidbody.velocity.y);
        }
    }
}