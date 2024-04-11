using _Scripts.PlayerScripts;

namespace _Scripts.UI.Widgets
{
    public class ControlPanelAnimatedWindow : NoteBookAnimatedWindow
    {
        protected override void OnEnable()
        {
            HeroInputHandler.OnConsoleToggle += ToggleWindow;
        }
        protected override void OnDisable()
        {
            HeroInputHandler.OnConsoleToggle += ToggleWindow;
        }
    }
}