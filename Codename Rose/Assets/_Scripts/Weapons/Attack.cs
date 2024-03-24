using UnityEngine;

namespace _Scripts.Weapons
{
    public class Attack
    {
        public Vector2 Position { get; }

        public AttackTypes AttackType { get; }

        public Attack(AttackTypes attackType,Vector2 position)
        {
            Position = position;
            AttackType = attackType;
        }
    }
}