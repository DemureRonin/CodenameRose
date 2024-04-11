using _Scripts.MapGeneration.Map;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class CoreWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            SetCoreValue();
        }

        private void SetCoreValue()
        {
            _text.text = "Core: " + MapState.CoreValue + "%";
        }

        private void OnEnable()
        {
            MapState.OnCoreValueChanged += SetCoreValue;
        }

        private void OnDisable()
        {
            MapState.OnCoreValueChanged -= SetCoreValue;
        }
    }
}