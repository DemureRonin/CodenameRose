using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float _followSpeed;
        [SerializeField] protected float _idleTimeToSwitchState;
        [SerializeField] protected float _attackSpeed;

        [SerializeField] protected Animator _weaponAnimator;
        [SerializeField] protected SwordModifyHealthComponent _swordModifyHealthComponent;

        [SerializeField] protected LayerMask _layerToHit;
        public abstract void OnAttack(AttackTypes attackType);
        protected bool _isInCombat;
        public bool IsInCombat => _isInCombat;
    }

    public enum AttackTypes
    {
        Heavy,
        Light
    }
}