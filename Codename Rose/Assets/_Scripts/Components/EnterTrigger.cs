using _Scripts.MapGeneration;
using UnityEngine;

namespace _Scripts.Components
{
    public class EnterTrigger : MonoBehaviour
    {
        [SerializeField] private Actions[] _actions;

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            foreach (var action in _actions)
            {
                if (!otherCollider.CompareTag(action.OtherTag)) continue;
                action.GameEvent.Invoke(otherCollider.gameObject);
                return;
            }
        }
    }
}