using System;
using UnityEngine;

namespace _Scripts.Components.Health
{
    public static class DamageCalculator
    {
        private const float ShortDistance = 2;
        private const float ShortDistanceMod = 1.2f;

        private const float MediumDistance = 5;
        private const float MediumDistanceMod = 1f;

        private const float LongDistance = 7;
        private const float LongDistanceMod = 0.8f;
        private const float LargeDistanceMod = 0.3f;

        public static float CalculateDamage(Vector2 recipientPosition, Vector2 attackerPosition,
            Vector2 previousSwordPosition, Vector2 swordPosition, double baseDamage)
        {
            var playerDistance = Vector2.Distance(recipientPosition, attackerPosition);
            
            var distanceModifier = playerDistance switch
            {
                < ShortDistance => ShortDistanceMod,
                >= ShortDistance and < MediumDistance => MediumDistanceMod,
                >= MediumDistance and < LongDistance => LongDistanceMod,
                >= LongDistance => LargeDistanceMod,

                _ => 1
            };

            var swordDistance = Vector2.Distance(previousSwordPosition, swordPosition);
            if (swordDistance <= 1) swordDistance = 0;
            swordDistance *= 0.2f;
            
            
            
            var damage =  baseDamage * swordDistance * distanceModifier;

            return (float)Math.Round(damage);
        }
    }
}