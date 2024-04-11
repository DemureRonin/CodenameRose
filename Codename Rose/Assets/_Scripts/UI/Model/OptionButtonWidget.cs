using UnityEngine;

namespace _Scripts.UI.Model
{
    public class OptionButtonWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _frame;
        [SerializeField] private GameObject _content;
        [SerializeField] private ItemTypes _type;

        public ItemTypes Type => _type;

        private ButtonsModel _model;

        public ButtonsModel Model => _model;


        private void Awake()
        {
            _model = GetComponentInParent<ButtonsModel>();
            _model.AddButton(this);
        }

        public void SetType(ItemTypes type)
        {
            _type = type;
        }

        public void Activate()
        {
            _model.DeactivateAllButtons();
            _frame.SetActive(true);
            if (_content != null)
                _content.SetActive(true);
        }

        public void Deactivate()
        {
            _frame.SetActive(false);
            if (_content != null)
                _content.SetActive(false);
        }
    }
}