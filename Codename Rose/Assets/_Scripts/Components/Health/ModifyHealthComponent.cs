using UnityEngine;

namespace _Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

      
        public void ModifyHealth(GameObject recipient, GameObject attacker)
        {
            var healthComponent = recipient.GetComponent<HealthComponent>();
            if (healthComponent == null) return;
            healthComponent.TakeDamage(_damage,attacker);
        }
    }
}