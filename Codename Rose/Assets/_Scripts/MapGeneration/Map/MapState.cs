using System;

namespace _Scripts.MapGeneration.Map
{
    public static class MapState
    {
        private static int _coreValue = 0;


        private static bool _day = true;

        private static bool _rain = false;
        public static bool Day => _day;

        public static bool Rain => _rain;
        public static int CoreValue => _coreValue;

        public delegate void MapEvent();

        public static event MapEvent OnCoreOverflow;
        public static event MapEvent OnCoreValueChanged;
        public static event MapEvent OnTimeChanged;
        public static event MapEvent OnWeatherChanged;


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
    }
}