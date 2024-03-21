using System.Collections;
using UnityEngine;

namespace _Scripts.VFX
{
    public class BlinkVisualEffect : MonoBehaviour
    {
        private const int AlphaIncrement = 10;
        private const int MinAlpha = 0;

         private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
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
            
            var minAlpha = new Color(initialColor.r,initialColor.g,initialColor.b,MinAlpha);
            _spriteRenderer.color = minAlpha;
            
            while (_spriteRenderer.color != initialColor)
            {
                tick += Time.deltaTime * AlphaIncrement;
                _spriteRenderer.color = Color.Lerp(minAlpha, initialColor, tick);
                yield return null;
            }
        }
    }
}