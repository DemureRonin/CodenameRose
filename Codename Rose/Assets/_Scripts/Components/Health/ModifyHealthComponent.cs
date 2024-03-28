using UnityEngine;

namespace _Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private float _damage;
        
        public  void ModifyHealth(GameObject recipient)
        {
            var healthComponent = recipient.GetComponent<HealthComponent>();
            if (healthComponent == null) return;
             
            healthComponent.TakeDamage(_damage, gameObject);
        }
    }
}