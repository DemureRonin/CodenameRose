using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class NoteBookAnimatedWindow : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        private bool _active;

        protected void ToggleWindow()
        {
            _active = !_active;
            _animator.Play(_active ? "show" : "hide");
        }

        protected virtual void OnEnable()
        {
            HeroInputHandler.OnNoteBookToggle += ToggleWindow;
        }

        protected virtual void OnDisable()
        {
            HeroInputHandler.OnNoteBookToggle -= ToggleWindow;
        }
    }
}