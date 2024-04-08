using _Scripts.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(Hero))]
    public class HeroInputHandler : MonoBehaviour
    {
        private Hero _hero;
        public delegate void InputEvent();
        public static event InputEvent OnNoteBookToggle;

        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }

        public void HorizontalMovement(InputAction.CallbackContext context)
        {
            var vector = context.ReadValue<Vector2>();
            _hero.SetVector(vector);
        }
        
        public void OnLeftMousePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack(AttackTypes.Light);
            }
        }

        public void OnRightMousePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack(AttackTypes.Heavy);
            }
        }

        public void OnShiftPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_hero.Dash());
            }
        }
        public void OnTabPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnNoteBookToggle?.Invoke();
            }
        }
    }
}