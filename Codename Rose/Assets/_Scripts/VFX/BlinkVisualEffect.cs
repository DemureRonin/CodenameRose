using System.Collections;
using UnityEngine;

namespace _Scripts.VFX
{
    public class BlinkVisualEffect : MonoBehaviour
    {
        [SerializeField] private int _alphaIncrement;
        [SerializeField] private int _minAlpha;
        [SerializeField] private float _updateFrequency;
        
        private SpriteRenderer _spriteRenderer;
        private WaitForSeconds _delay;

        private void Awake()
        {
            _delay = new WaitForSeconds(_updateFrequency);
            _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        }

        public void Blink()
        {
            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            var initialColor = _spriteRenderer.color;
            float tick = 0f;
            
            var minAlpha = new Color(initialColor.r,initialColor.g,initialColor.b,_minAlpha);
            _spriteRenderer.color = minAlpha;
            
            while (_spriteRenderer.color != initialColor)
            {
                tick += Time.deltaTime * _alphaIncrement;
                _spriteRenderer.color = Color.Lerp(minAlpha, initialColor, tick);
                yield return null;
            }
        }
    }
}