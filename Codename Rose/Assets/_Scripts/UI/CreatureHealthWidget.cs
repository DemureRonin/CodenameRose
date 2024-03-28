using System.Globalization;
using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.UI
{
    public class CreatureHealthWidget : HeroHealthWidget
    {
        [SerializeField] private HealthComponent _healthComponent;

        private void Start()
        {
            SetHpBar();
        }

        public void SetHpBar()
        {
            _image.SetProgress( _healthComponent.Health / _healthComponent.MaxHp);
            _text.text =  _healthComponent.Health.ToString(CultureInfo.InvariantCulture) + "/" +
                          _healthComponent.MaxHp.ToString(CultureInfo.InvariantCulture);
        }

        protected override void OnEnable()
        {
        }
        protected override void OnDisable()
        {
        }
    }
}