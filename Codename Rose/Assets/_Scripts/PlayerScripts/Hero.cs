using System.Collections;
using _Scripts.Creatures;
using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class Hero : Creature
    {
        [SerializeField] private float _dashSpeed;

        [SerializeField] private LayerMask _collectableLayer;
        [SerializeField] private Weapon _equippedWeapon;


        private float _radius = 1;
        private Collider2D[] _interactionResult;
        private bool _isDashing;


        protected override void FixedUpdate()
        {
            if (_isDashing) return;
            _movementSpeed = _equippedWeapon.IsInCombat ? 1.2f :2.3f; 
            base.FixedUpdate();
        }
        public void SetVector(Vector2 vector)
        {
            _movementVector = vector;
            if (vector != Vector2.zero)
            {
                _lookDirection = vector;
            }
        }


        public void CheckCollectableObjects()
        {
            var size = Physics2D.OverlapCircle(transform.position, _radius, _collectableLayer);
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