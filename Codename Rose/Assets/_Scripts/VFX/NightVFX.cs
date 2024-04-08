using System.Collections;
using _Scripts.MapGeneration.Map;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Scripts.VFX
{
    public class NightVFX : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private Color _nightColor;

        private void SetNight()
        {
            StartCoroutine(ChangeLightColor());
        }

        private IEnumerator ChangeLightColor()
        {
            var lightIncrement = 1;
            var initialColor = _light.color;
            float tick = 0f;


            while (_light.color != _nightColor)
            {
                tick += Time.deltaTime * lightIncrement;
                _light.color = Color.Lerp(initialColor, _nightColor, tick);
                yield return null;
            }
        }

        private void OnEnable()
        {
            MapState.OnTimeChanged += SetNight;
        }

        private void OnDisable()
        {
            MapState.OnTimeChanged -= SetNight;
        }
    }
}