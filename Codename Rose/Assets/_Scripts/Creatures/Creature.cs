using _Scripts.Components;
using _Scripts.Components.Health;
using _Scripts.Creatures.CreatureDef;
using UnityEngine;

namespace _Scripts.Creatures
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Creature : MonoBehaviour 
    {
        private CreatureParameterDef _creatureParameters;
        private SpriteRenderer _spriteRenderer;

        protected Vector2 MovementVector;
        protected Vector2 LookDirection;

        protected Rigidbody2D RigidBody;
        protected Animator Animator;
        protected HealthComponent HealthComponent;

        private float MovementSpeed => _creatureParameters.MovementSpeed;

        private static readonly int XDirection = Animator.StringToHash("xDirection");
        private static readonly int YDirection = Animator.StringToHash("yDirection");
        private const string ResourcePath = "DefaultCreatureParams";

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            HealthComponent = GetComponentInChildren<HealthComponent>();
            HealthComponent.SetHealth(_creatureParameters.MaxHealth);
            Animator = GetComponent<Animator>();
            RigidBody = GetComponent<Rigidbody2D>();
            RigidBody.gravityScale = 0;
            RigidBody.freezeRotation = true;

            if (_creatureParameters != null) return;
            Debug.LogWarning("Loaded Creature with default parameters at" + gameObject.name);
            _creatureParameters = (CreatureParameterDef)Resources.Load(ResourcePath);
        }

        protected void InitializeParameters(CreatureParameterDef parameters)
        {
            _creatureParameters = parameters;
        }

        protected virtual void FixedUpdate()
        {
            RigidBody.velocity = new Vector2(MovementVector.x * MovementSpeed, MovementVector.y * MovementSpeed);
            Animate();
        }

        private void Update()
        {
            if (RigidBody.velocity != Vector2.zero)
                LookDirection = RigidBody.velocity;

            SortOrderInLayer();
        }

        private void SortOrderInLayer()
        {
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * RenderSorting.SortingDiscretion) + 3;
        }

        private void Animate()
        {
            Animator.SetFloat(XDirection, LookDirection.x);
            Animator.SetFloat(YDirection, LookDirection.y);
        }
    }
}