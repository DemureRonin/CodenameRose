using UnityEngine;

namespace _Scripts.Components
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        public void Spawn(GameObject other)
        {
            Instantiate(_prefab, other.transform.position, Quaternion.identity);
        }
        public void Spawn(Vector3 position, Quaternion rotation)
        {
            Instantiate(_prefab, position, rotation);
        }
        public void Spawn()
        {
            Instantiate(_prefab, transform.position, Quaternion.identity);
        }
    }
}