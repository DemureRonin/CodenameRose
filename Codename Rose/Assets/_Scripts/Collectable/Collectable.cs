using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Collectable
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private UnityEvent _onCollect;

        public delegate void CollectEvent(Collectable collectable);

        public static event CollectEvent OnCollect;


        public void Collect()
        {
            OnCollect?.Invoke(this);
            _onCollect?.Invoke();
        }
    }
}