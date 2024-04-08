using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI
{
    public class NoteBookWidget : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private bool _active;

        private void ToggleNoteBook()
        {
            _active = !_active;
            _animator.Play(_active ? "show" : "hide");
        }

        private void OnEnable()
        {
            HeroInputHandler.OnNoteBookToggle += ToggleNoteBook;
        }

        private void OnDisable()
        {
            HeroInputHandler.OnNoteBookToggle -= ToggleNoteBook;
        }
    }
}