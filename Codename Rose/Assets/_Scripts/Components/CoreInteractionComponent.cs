using System;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Components
{
    public class CoreInteractionComponent : MonoBehaviour
    {
        private bool _canInteract;
        public delegate void InputEvent();
        public static event InputEvent OnConsoleToggle;

        public void SetInteraction()
        {
            OnConsoleToggle?.Invoke();
        }
    }
    
}