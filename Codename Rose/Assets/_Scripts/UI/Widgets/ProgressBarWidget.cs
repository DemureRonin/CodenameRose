using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Widgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        private Image _bar;

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
    }
}