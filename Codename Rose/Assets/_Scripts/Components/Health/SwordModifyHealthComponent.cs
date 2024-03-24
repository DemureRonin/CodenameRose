using UnityEngine;

namespace _Scripts.Components.Health
{
    public class SwordModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private float _baseDamage = 1;


        public void ModifyHealth(GameObject recipient, GameObject attacker, Vector2 previousSwordPosition,
            Vector2 swordPosition)
        {
            var healthComponent = recipient.GetComponent<HealthComponent>();
            if (healthComponent == null) return;

            var damage = DamageCalculator.CalculateDamage(
                recipient.transform.position,
                attacker.transform.position,
                previousSwordPosition,
                swordPosition,
                _baseDamage);
            healthComponent.TakeDamage(damage, attacker);
        }
    }
}