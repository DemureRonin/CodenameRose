using System;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.VFX
{
    public class CollectableVFX : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody2D;
        private Timer _timer = new Timer();

        private void Awake()
        {
            _timer.Value = 1f;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Pop();
            _timer.StartTimer();
        }

        private void Update()
        {
            if (_timer.IsReady)
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        private void Pop()
        {
            _rigidbody2D.AddForce(Vector2.right * _speed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}