using UnityEngine;

namespace _Scripts.Components
{
    public class DestroyComponent : MonoBehaviour
    {
        [SerializeField] private bool _destroyThisGameObject;
        [SerializeField] private GameObject _objectToDestroy;

        public void Destroy()
        {
            Destroy(_destroyThisGameObject ? gameObject : _objectToDestroy);
        }
    }
}