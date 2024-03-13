using UnityEngine;

namespace _Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Attack();
        public abstract void FollowPlayer();
    }
}