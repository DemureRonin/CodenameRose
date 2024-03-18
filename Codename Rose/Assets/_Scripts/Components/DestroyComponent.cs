using System.Collections;
using UnityEngine;

namespace _Scripts.Components
{
    public class DestroyComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        [SerializeField] private bool _destroySelf;
        [SerializeField] private bool _destroyWithDelay;
        [SerializeField] private bool _destroyOnStartWithDelay;
        [SerializeField] private float _delayTime;

        private Coroutine _coroutine;
        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(_delayTime);
            if (_destroyOnStartWithDelay)
            {
                _coroutine = StartCoroutine(DestroyCoroutine());
            }
        }

        public void DestroyWithDelay()
        {
            if (_coroutine != null) return;
            StartCoroutine(DestroyCoroutine());
        }

        public void Destroy()
        {
            Destroy(_objectToDestroy);
        }

        private IEnumerator DestroyCoroutine()
        {
            if (_destroyWithDelay)
            {
                yield return _delay;
            }

            Destroy(_destroySelf ? gameObject : _objectToDestroy);
        }
    }
}