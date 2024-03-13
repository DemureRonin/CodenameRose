using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(Hero))]
    public class HeroInputHandler : MonoBehaviour
    {
        private Hero _hero;

        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }

        public void HorizontalMovement(InputAction.CallbackContext context)
        {
            var vector = context.ReadValue<Vector2>();
            _hero.SetVector(vector);
        }
        public void OnQButtonPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.CheckCollectableObjects();
            }
        }

        public void OnLeftMousePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }
    }
}
