using System;
using UnityEngine;

namespace _Scripts.Components.Health
{
    public static class DamageCalculator
    {
        

        public static float CalculateDamage(Vector2 recipientPosition, Vector2 attackerPosition,
            Vector2 previousSwordPosition, Vector2 swordPosition, double baseDamage)
        {
            var playerDistance = Vector2.Distance(recipientPosition, attackerPosition);
            
            var damage =  4/ playerDistance * baseDamage;

            return (float)Math.Round(damage);
        }
    }
}