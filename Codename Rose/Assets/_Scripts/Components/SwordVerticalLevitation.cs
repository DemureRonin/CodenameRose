using _Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Components
{
    public class SwordVerticalLevitation : VerticalLevitation
    {
        [FormerlySerializedAs("_sword")] [SerializeField] private LightSword _lightSword;

        protected override void Update()
        {
            if (_lightSword.IsInCombat) return;
            base.Update();
        }
    }
}