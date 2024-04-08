using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class CameraFollow : MonoBehaviour
    {
        private CinemachineVirtualCamera _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            _camera.Follow = FindObjectOfType<Hero>().transform;
        }

        private void OnEnable()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _camera.Follow = FindObjectOfType<Hero>()?.transform;
        }

        private void OnDisable()
        {
            _camera.Follow = null;
        }
    }
}