using System.Collections;
using _Scripts.Creatures;
using _Scripts.Creatures.CreatureDef;
using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class Hero : Creature
    {
        [SerializeField] private HeroParameterDef _heroParameterDef;
        [SerializeField] private LayerMask _collectableLayer;
        [SerializeField] private Weapon _equippedWeapon;

        private float DashSpeed => _heroParameterDef.DashSpeed;
        private float CollectRadius => _heroParameterDef.CollectRadius;

        private Collider2D[] _interactionResult;
        private bool _isDashing;

        protected override void Awake()
        {
            InitializeParameters(_heroParameterDef);
            base.Awake();
        }
        
        protected override void FixedUpdate()
        {
            if (_isDashing) return;
            base.FixedUpdate();
        }

        public void SetVector(Vector2 vector)
        {
            MovementVector = vector;
            if (vector != Vector2.zero)
            {
                LookDirection = vector;
            }
        }

        public void CheckCollectableObjects()
        {
            var size = Physics2D.OverlapCircle(transform.position, CollectRadius, _collectableLayer);
            if (size == null) return;
            var coll = size.gameObject.GetComponent<Collectable.Collectable>();
            coll.Collect();
        }

        public void Attack(AttackTypes attackType) => _equippedWeapon.OnAttack(attackType);

        public IEnumerator Dash()
        {
            _isDashing = true;
            RigidBody.AddForce(LookDirection * DashSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
            RigidBody.velocity = Vector2.zero;
            _isDashing = false;
        }
    }
}