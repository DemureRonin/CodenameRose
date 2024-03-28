using UnityEngine;

namespace _Scripts.Creatures.CreatureDef
{
    [CreateAssetMenu(menuName = "NPCParameters", fileName = "CreatureDef")]
    public class NonPlayableCreatureParameterDef : CreatureParameterDef
    {
        [Range(1, 5)] [SerializeField] private int _level;
        public int Level => _level;
    }
}