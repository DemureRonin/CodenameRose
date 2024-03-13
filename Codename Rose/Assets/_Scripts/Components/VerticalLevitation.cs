using UnityEngine;

namespace _Scripts.Components
{
    public class VerticalLevitation : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private bool _randomize;

        private float _originalY;
        private Rigidbody2D _rigidbody;
        private float _seed;

        private void Awake()
        {
            _originalY = transform.position.y;
            if (_randomize)
                _seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var position = transform.position;
            position.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
            var positionToMove = new Vector2(position.x, position.y);
            transform.position = Vector2.MoveTowards(position, positionToMove, _frequency);
        }
    }
}