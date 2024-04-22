using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.VFX
{
    public class CollectableVFX : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Hero _hero;

        private void Awake()
        {
            _hero = FindAnyObjectByType<Hero>();
        }
        
        private void Update()
        {
           transform.position = Vector2.Lerp(transform.position, _hero.transform.position, _speed* Time.deltaTime);
        }
    }
}