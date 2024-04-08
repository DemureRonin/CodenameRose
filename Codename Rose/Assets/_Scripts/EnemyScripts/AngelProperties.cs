using System;
using _Scripts.MapGeneration.Map;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.EnemyScripts
{
    public class AngelProperties : MonoBehaviour
    {
        [SerializeField] private bool _daySpawn;
        [SerializeField] private bool _rainSpawn;
        [SerializeField] private int _coreTransmissionValue;
        [SerializeField] private GameObject _angelObject;

        private void Start()
        {
            OnMapStateChanged();
        }

        private void OnMapStateChanged()
        {
            if (_daySpawn == MapState.Day && _rainSpawn == MapState.Rain)
            {
                _angelObject.SetActive(true);
                return;
            }

            _angelObject.SetActive(false);
        }

        public void TransmitToCore()
        {
            MapState.SetCoreValue(_coreTransmissionValue);
        }

        private void OnEnable()
        {
            MapState.OnTimeChanged += OnMapStateChanged;
            MapState.OnWeatherChanged += OnMapStateChanged;
        }

        private void OnDisable()
        {
            MapState.OnTimeChanged -= OnMapStateChanged;
            MapState.OnWeatherChanged -= OnMapStateChanged;
        }
    }
}