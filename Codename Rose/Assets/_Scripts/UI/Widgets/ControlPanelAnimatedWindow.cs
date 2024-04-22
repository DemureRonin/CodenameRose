using _Scripts.Components;

namespace _Scripts.UI.Widgets
{
    public class ControlPanelAnimatedWindow : NoteBookAnimatedWindow
    {
        private void Hide()
        {
            _animator.Play("hide");
        }
        protected override void OnEnable()
        {
            CoreInteractionComponent.OnConsoleToggle += ToggleWindow;
        }
        protected override void OnDisable()
        {
            CoreInteractionComponent.OnConsoleToggle -= ToggleWindow;
        }
    }
}