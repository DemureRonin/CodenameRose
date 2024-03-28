using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.Components.Health
{
    public class SwordModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private float _baseDamageLight = 1;
        [SerializeField] private float _baseDamageHeavy = 1;


        public void ModifyHealth(GameObject recipient, GameObject attacker, Vector2 previousSwordPosition,
            Vector2 swordPosition, AttackTypes attackType)
        {
            var healthComponent = recipient.GetComponent<HealthComponent>();
            if (healthComponent == null) return;
            float damage = _baseDamageLight;
            switch (attackType)
            {
                case AttackTypes.Light:
                    damage = DamageCalculator.CalculateDamage(
                        recipient.transform.position,
                        attacker.transform.position,
                        previousSwordPosition,
                        swordPosition,
                        _baseDamageLight);
                    break;
                case AttackTypes.Heavy:
                    damage = _baseDamageHeavy;
                    break;
            }


            healthComponent.TakeDamage(damage, attacker);
        }
    }
}