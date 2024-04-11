using System.Collections.Generic;
using _Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.UI.Model
{
    public class ItemOptionButtonModel : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _group;
        [SerializeField] private OptionButtonDef _optionButtonDef;

        private List<ItemTypes> _buttons = new();

        private void Awake()
        {
            foreach (var item in _inventory.Items)
            {
                if (item.Obtained) AddButton(item.ID);
            }
        }

        private void AddButton(ItemTypes type)
        {
            if (_buttons.Contains(type)) return;
            foreach (var button in _optionButtonDef.Buttons)
            {
                if (button.Type != type) continue;
               var obj= Instantiate(button.ButtonObject, _group.transform);
                obj.GetComponent<OptionButtonWidget>().SetType(type);
                _buttons.Add(type);
                return;
            }
        }


        private void OnEnable()
        {
            Collectable.Collectable.OnCollect += AddButton;
        }

        private void OnDisable()
        {
            Collectable.Collectable.OnCollect -= AddButton;
        }
    }
}