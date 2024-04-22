using System;
using _Scripts.MapGeneration.Map;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class YearWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _yearText;
        [SerializeField] private TextMeshProUGUI _yearsPassedText;

        private void SetYear()
        {
            _yearText.text = "Current Year: " + MapState.Year;
            _yearsPassedText.text = "Years in the Loop: " + MapState.YearsPassed;
        }

        private void OnEnable()
        {
            MapState.OnYearChanged += SetYear;
        }

        private void OnDisable()
        {
            MapState.OnYearChanged -= SetYear;
        }
    }
}