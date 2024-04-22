using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.EnemyScripts
{
    public class AngelDeathObserver : MonoBehaviour
    {
        private int _counter;
        private void CountDeath()
        {
            _counter++;
            if (_counter == 4)
            {
                SceneManager.LoadScene("End");
            }
        }

        private void OnEnable()
        {
            AngelAI.OnDeath += CountDeath;
        }

        private void OnDisable()
        {
            AngelAI.OnDeath -= CountDeath;
        }
    }
}