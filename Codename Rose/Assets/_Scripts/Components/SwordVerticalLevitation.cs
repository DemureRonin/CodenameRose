using _Scripts.Weapons;
using UnityEngine;

namespace _Scripts.Components
{
    public class SwordVerticalLevitation : VerticalLevitation
    {
        [SerializeField] private Sword _sword;

        protected override void Update()
        {
            if (_sword.IsInCombat) return;
            base.Update();
        }
    }
}