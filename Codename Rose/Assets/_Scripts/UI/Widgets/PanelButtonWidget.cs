using System;
using _Scripts.UI.Model;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class PanelButtonWidget : MonoBehaviour
    {
        private OptionButtonWidget _widget;

        public delegate void PressEvent(ItemTypes id);

        public static event PressEvent OnPress;

        private void Awake()
        {
            _widget = GetComponent<OptionButtonWidget>();
        }

        public void Press()
        {
            OnPress?.Invoke(_widget.Type);
        }

        private void DeleteButton(ItemTypes? type)
        {
            if (type == _widget.Type)
                _widget.Model.RemoveButton(_widget);
        }

        private void OnEnable()
        {
            PanelSlotWidget.OnPlace += DeleteButton;
        }

        private void OnDisable()
        {
            PanelSlotWidget.OnPlace -= DeleteButton;
        }
    }
}