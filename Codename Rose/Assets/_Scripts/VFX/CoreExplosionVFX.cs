using System;
using _Scripts.MapGeneration.Map;
using UnityEngine;

namespace _Scripts.VFX
{
    public class CoreExplosionVFX : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Explode()
        {
            _animator.Play("explode");
        }

        private void OnEnable()
        {
            MapState.OnCoreOverflow += Explode;
        }

        
        private void OnDisable()
        {
            MapState.OnCoreOverflow -= Explode;
        }
    }
}