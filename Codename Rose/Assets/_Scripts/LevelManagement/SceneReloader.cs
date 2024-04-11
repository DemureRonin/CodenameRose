using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.LevelManagement
{
    public class SceneReloader : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}