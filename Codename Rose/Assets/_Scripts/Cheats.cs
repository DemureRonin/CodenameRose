using _Scripts.MapGeneration.Map;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class Cheats : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                MapState.SetRain();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                MapState.SetNight();
            }
        }
    }
}