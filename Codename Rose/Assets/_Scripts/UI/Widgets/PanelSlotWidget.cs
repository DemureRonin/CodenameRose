using System;
using _Scripts.UI.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Widgets
{
    public class PanelSlotWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _blockerImage;
        [SerializeField] private bool _reductor;
        [SerializeField] private OptionButtonDef _optionButtonDef;
        [SerializeField] private ControlPanelModel _controlPanelModel;
        [SerializeField] private int _lane;
        private bool _active;
        private Image _image;
        private ItemTypes? _currentItemSelection;
        private ItemTypes? _placedObject;
        private bool _occupied;

        public delegate void PlaceEvent(ItemTypes? id);

        public static event PlaceEvent OnPlace;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonUp(0) && _active)
            {
                // _blockerImage.SetActive(true);
            }
        }

        public void PlaceObject()
        {
            if (!_active) return;
            if (_occupied) return;
            if (_currentItemSelection == null) return;

            foreach (var button in _optionButtonDef.Buttons)
            {
                if (button.Type != _currentItemSelection) continue;
                
                _image.sprite = button.Sprite;
                _image.enabled = true;
                _placedObject = _currentItemSelection;
                _occupied = true;

                OnPlace?.Invoke(_placedObject);
                
                if (_reductor) _controlPanelModel.SetReductor(_placedObject, _lane);
                else _controlPanelModel.SetModule(_placedObject);
                
                return;
            }
        }


        private void Activate(ItemTypes id)
        {
            if (_occupied) return;
            if (_reductor == CheckType(id))
            {
                _blockerImage.SetActive(false);
                _currentItemSelection = id;
                _active = true;
                return;
            }

            _blockerImage.SetActive(true);
            _active = false;
        }

        private bool CheckType(ItemTypes id) => id is ItemTypes.CoreReductor or ItemTypes.ElectricityReductor;

        private void ClearSelection(ItemTypes? id)
        {
            _currentItemSelection = null;
        }

        private void OnEnable()
        {
            PanelButtonWidget.OnPress += Activate;
            OnPlace += ClearSelection;
        }

        private void OnDisable()
        {
            PanelButtonWidget.OnPress -= Activate;
            OnPlace -= ClearSelection;
        }

        
    }
}