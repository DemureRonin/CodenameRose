using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class HeroSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _hero;

        private void Awake()
        {
            SpawnHero();
        }

        public void SpawnHero()
        {
            Instantiate(_hero, transform.position, Quaternion.identity);
        }
    }
}