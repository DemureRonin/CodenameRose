using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class Cheats : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}