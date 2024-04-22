using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class DeathScreenWidget : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private void Start()
        {
            _animator.Play("awake");
        }

        private void Show()
        {
            _animator.Play("die");
        }

        private void OnEnable()
        {
            HeroDeathObserver.OnHeroDeath += Show;
        }

        private void OnDisable()
        {
            HeroDeathObserver.OnHeroDeath -= Show;
        }
    }
}