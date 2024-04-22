using System.Collections;
using _Scripts.MapGeneration.Map;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Scripts.VFX
{
    public class ChangeLightVFX : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private Color _nightColor;
        [SerializeField] private Color _rainColor;

        private void SetNight()
        {
            StartCoroutine(ChangeLightColor(_nightColor));
        }
        private void SetRain()
        {
            if (!MapState.Day) return;
            StartCoroutine(ChangeLightColor(_rainColor));
        }

        private IEnumerator ChangeLightColor(Color nextColor)
        {
            var lightIncrement = 1;
            var initialColor = _light.color;
            float tick = 0f;


            while (_light.color != nextColor)
            {
                tick += Time.deltaTime * lightIncrement;
                _light.color = Color.Lerp(initialColor, nextColor, tick);
                yield return null;
            }
        }

        private void OnEnable()
        {
            MapState.OnTimeChanged += SetNight;
            MapState.OnWeatherChanged += SetRain;
        }

        private void OnDisable()
        {
            MapState.OnTimeChanged -= SetNight;
            MapState.OnWeatherChanged -= SetRain;
        }
    }
}