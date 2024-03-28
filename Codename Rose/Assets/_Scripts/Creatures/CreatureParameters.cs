using _Scripts.Creatures.CreatureDef;
using UnityEngine;

namespace _Scripts.Creatures
{
    public class CreatureParameters : MonoBehaviour
    {
        [SerializeField] private CreatureParameterDef _creatureParameterDef;

        public CreatureParameterDef CreatureParameterDef => _creatureParameterDef;
    }

  
}