using System.Collections;
using _Scripts.MapGeneration.Map;
using _Scripts.UI.Widgets.AngelInfo;
using UnityEngine;

namespace _Scripts.EnemyScripts
{
    public class AngelProperties : MonoBehaviour
    {
        [SerializeField] private bool _daySpawn;
        [SerializeField] private bool _rainSpawn;
        [SerializeField] private int _coreTransmissionValue;
        [SerializeField] private int _lane;
        [SerializeField] private GameObject _angelObject;
       
        public bool CanTransmitToCore = true;
        public int Lane => _lane;

        private void Start()
        {
            OnMapStateChanged();
        }

        public void OnEncounter()
        {
            
        }

        private void OnMapStateChanged()
        {
            if (_daySpawn == MapState.Day && _rainSpawn == MapState.Rain)
            {
                _angelObject.SetActive(true);
                return;
            }

            if (_angelObject != null)
                _angelObject.SetActive(false);
        }

        public void TransmitToCore()
        {
            if (!CanTransmitToCore) return;
            StartCoroutine(TransmitValue());

            IEnumerator TransmitValue()
            {
                for (int i = 0; i < _coreTransmissionValue; i++)
                {
                    MapState.SetCoreValue(1);
                    yield return new WaitForSeconds(0.05f);
                }
            }
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

        public void SelfDestruct()
        {
            var ai = _angelObject.GetComponent<AngelAI>();
            ai.OnDie();
        }
    }
}