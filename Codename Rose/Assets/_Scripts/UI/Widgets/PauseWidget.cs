using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class PauseWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        private HeroInputHandler _input;
        private bool _paused;
        private void Start()
        {
            _input = FindObjectOfType<HeroInputHandler>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _paused = !_paused;
                Pause();
            }
        }

        private void Pause()
        {
            _pauseMenu.SetActive(_paused);
            Time.timeScale = _paused ? 0 : 1;
            _input.enabled = _paused;
        }
    }
}