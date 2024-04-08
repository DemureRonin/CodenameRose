using System;
using _Scripts.MapGeneration.Map;
using UnityEngine;

namespace _Scripts.VFX
{
    public class RainVFX : MonoBehaviour
    {
        [SerializeField] private GameObject _rainParticleSystem;

        private void SetRain()
        {
            _rainParticleSystem.SetActive(true);
        }

        private void OnEnable()
        {
            MapState.OnWeatherChanged += SetRain;
        }

        private void OnDisable()
        {
            MapState.OnWeatherChanged -= SetRain;
        }
    }
}