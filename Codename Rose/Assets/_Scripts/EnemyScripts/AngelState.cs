using _Scripts.MapGeneration.Map;
using UnityEngine;

namespace _Scripts.EnemyScripts
{
    [CreateAssetMenu(menuName = "AngelState")]
    public class AngelState : ScriptableObject
    {
        [SerializeField] private Angel[] _angels;

        public Angel[] Angels => _angels;
    }
}