using System;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.World
{
    [RequireComponent(typeof(Animator), typeof(TextMeshProUGUI))]
    public class LocationHeaderWidget : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private Animator _animator;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _animator = GetComponent<Animator>();
        }

        private void ShowHeader(string locationName)
        {
            _text.text = locationName;
            _animator.Play("show");
        }
        private void OnEnable()
        {
            BiomeObserver.OnExplore += ShowHeader;
        }

        private void OnDisable()
        {
            BiomeObserver.OnExplore -= ShowHeader;
        }
    }
}