using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class HeroHealthWidget : MonoBehaviour
    {
        [SerializeField] protected ProgressBarWidget _image;

        protected void SetHpBar(float currentHp, float maxHp)
        {
            _image.SetProgress(currentHp / maxHp);
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