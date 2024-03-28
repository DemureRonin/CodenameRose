using UnityEngine;

namespace _Scripts.Creatures.CreatureDef
{
      [CreateAssetMenu(menuName = "HeroParameters", fileName = "HeroDef")]
    public class HeroParameterDef : CreatureParameterDef
    {
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _collectRadius = 1;
        public float CollectRadius => _collectRadius;
        public float DashSpeed => _dashSpeed;
    }
}