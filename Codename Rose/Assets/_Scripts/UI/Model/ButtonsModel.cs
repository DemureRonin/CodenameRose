using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.UI.Model
{
    public class ButtonsModel : MonoBehaviour
    {
        [SerializeField] private List<OptionButtonWidget> _buttons;

        public void DeactivateAllButtons()
        {
            foreach (var button in _buttons)
            {
                button.Deactivate();
            }
        }

        public void AddButton(OptionButtonWidget buttonWidget)
        {
            _buttons.Add(buttonWidget);
        }

        public void RemoveButton(OptionButtonWidget buttonWidget)
        {
            _buttons.Remove(buttonWidget);
            Destroy(buttonWidget.gameObject);
        }
    }
}