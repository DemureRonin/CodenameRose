using System.Collections;
using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _followSpeed;
        [SerializeField] protected float _idleTimeToSwitchState;
        [SerializeField] protected float _maxAttackDistance;
        [SerializeField] protected float _attackSpeed;
        
        [SerializeField] protected Animator _weaponAnimator;
        [SerializeField] protected ModifyHealthComponent _modifyHealthComponent;
        
        [SerializeField] protected LayerMask _layerToHit;
        public abstract void Attack(AttackTypes attackType,Vector2 mousePosition = default);
        public abstract void FollowPlayer();
        public abstract IEnumerator LightAttack(Vector2 mousePosition);
        public abstract IEnumerator HeavyAttack(Vector2 mousePosition);
        
    }

    public enum AttackTypes
    {
        Heavy,
        Light
    }
}