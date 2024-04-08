using System.Collections;
using UnityEngine;

namespace _Scripts.VFX
{
    public class BlinkVisualEffect : MonoBehaviour
    {
        private const int AlphaIncrement = 10;
        private const int MinAlpha = 0;
        private bool _isBlinking;

         private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        }

        public void Blink()
        {
            if (_isBlinking) return;
            _isBlinking = true;
            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            var initialColor = _spriteRenderer.color;
            float tick = 0f;
            
            var minAlpha = new Color(initialColor.r,initialColor.g,initialColor.b,MinAlpha);
            _spriteRenderer.color = minAlpha;
            
            while (_spriteRenderer.color != initialColor)
            {
                tick += Time.deltaTime * AlphaIncrement;
                _spriteRenderer.color = Color.Lerp(minAlpha, initialColor, tick);
                yield return null;
            }

            _isBlinking = false;
        }
    }
}