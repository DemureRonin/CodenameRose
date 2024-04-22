using System;
using _Scripts.EnemyScripts;
using _Scripts.UI.Widgets.AngelInfo;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.MapGeneration.Map
{
    public class MapState : MonoBehaviour
    {
       [SerializeField] private AngelState _angels;

        public AngelState Angels => _angels;

        private static int _coreValue = 0;

        private static bool _day = true;

        private static bool _rain = false;
        public static bool Day => _day;

        public static bool Rain => _rain;
        public static int CoreValue => _coreValue;
        private static int _year = 2934;
        private static int _yearsPassed = 287;

        public static int Year => _year;

        public static int YearsPassed => _yearsPassed;

        public delegate void MapEvent();
        public delegate void AngelEvent(AngelNames name);

        public static event MapEvent OnCoreOverflow;
        public static event MapEvent OnCoreValueChanged;
        public static event MapEvent OnTimeChanged;
        public static event MapEvent OnWeatherChanged;
        public static event MapEvent OnYearChanged;
        public static event AngelEvent OnAngelStatusChanged;

        private void Awake()
        {
            _coreValue = 0;
            _day = true;
            _rain = false;
            foreach (var angel in _angels.Angels)
            {
                angel._alive = true;
            }
        }

        public static void SetNight()
        {
            _day = false;
            OnTimeChanged?.Invoke();
        }

        public static void SetRain()
        {
            _rain = true;
            OnWeatherChanged?.Invoke();
        }

        public static void SetCoreValue(int value)
        {
            if (value < 0) throw new ArgumentException("Core value passed is less that 0");
            _coreValue += value;
            OnCoreValueChanged?.Invoke();
            if (_coreValue > 100) OnCoreOverflow?.Invoke();
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _year++;
            _yearsPassed++;
            OnYearChanged?.Invoke();
        }

        private void SetAngelInfo(AngelNames angelNames)
        {
            foreach (var angel in _angels.Angels)
            {
                if (angelNames == angel._name)
                {
                    angel._encountered = true;
                    OnAngelStatusChanged?.Invoke(angelNames);
                }
            }
        }

        private void SetAngelStatus(AngelNames angelNames)
        {
            foreach (var angel in _angels.Angels)
            {
                if (angelNames == angel._name)
                {
                    angel._alive = false;
                    OnAngelStatusChanged?.Invoke(angelNames);
                }
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            AngelAI.OnEncounter += SetAngelInfo;
            AngelAI.OnAngelDeath += SetAngelStatus;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            AngelAI.OnEncounter -= SetAngelInfo;
            AngelAI.OnAngelDeath -= SetAngelStatus;
        }
    }

    [Serializable]
    public class Angel
    {
        public AngelNames _name;
        public bool _encountered;
        public bool _alive;
    }


    public enum AngelNames
    {
        Phanuel,
        Michael,
        Gabriel,
        Raphael
    }
}