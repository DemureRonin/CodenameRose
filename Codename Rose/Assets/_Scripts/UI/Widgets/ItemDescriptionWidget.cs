using System;
using _Scripts.UI.Model;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class ItemDescriptionWidget : MonoBehaviour
    {
        [SerializeField] private Descriptions[] _descriptions;
        private void ShowDescription(ItemTypes id)
        {
            foreach (var description in _descriptions)
            {
                description.Text.gameObject.SetActive(false);
                if (description.Type == id)
                {
                    description.Text.gameObject.SetActive(true);
                }
            }
        }

        private void OnEnable()
        {
            OptionButtonWidget.OnSelect += ShowDescription;
        }
        private void OnDisable()
        {
            OptionButtonWidget.OnSelect -= ShowDescription;
        }
    }

    [Serializable]
    public class Descriptions
    {
        [SerializeField] private ItemTypes _type;
        [SerializeField] private TextMeshProUGUI _text;

        public ItemTypes Type => _type;

        public TextMeshProUGUI Text => _text;
    }
}