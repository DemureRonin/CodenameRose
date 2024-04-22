using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class HeroDeathObserver : MonoBehaviour
    {
        public delegate void HeroDeathEvent();

        public static event HeroDeathEvent OnHeroDeath;
        public void OnDeath()
        {
            OnHeroDeath?.Invoke(); 
        }
    }
}