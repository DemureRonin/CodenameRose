using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class HeroHealthWidget : MonoBehaviour
    {
        [SerializeField] protected ProgressBarWidget _image;
        [SerializeField] protected TextMeshProUGUI _text;

        private void SetHpBar(float currentHp, float maxHp)
        {
            _image.SetProgress(currentHp / maxHp);
            _text.text = currentHp.ToString(CultureInfo.InvariantCulture) + "/" +
                         maxHp.ToString(CultureInfo.InvariantCulture);
        }

        protected virtual void OnEnable()
        {
            HealthChangeObserver.OnHealthChanged += SetHpBar;
        }

        protected virtual void OnDisable()
        {
            HealthChangeObserver.OnHealthChanged -= SetHpBar;
        }
    }
}