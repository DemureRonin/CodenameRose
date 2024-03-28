using UnityEngine;

namespace _Scripts.Creatures.CreatureDef
{
    [CreateAssetMenu(menuName = "CreatureParameters", fileName = "CreatureDef")]
    public class CreatureParameterDef : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _maxHealth;

        public float MovementSpeed => _movementSpeed;
        public float MaxHealth => _maxHealth;
       
    }
}