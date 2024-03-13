using _Scripts.MapGeneration;
using UnityEngine;

namespace _Scripts.Components
{
    public class ExitTrigger : MonoBehaviour
    {
        [SerializeField] protected Actions[] _actions;

        private void OnTriggerExit2D(Collider2D otherCollider)
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