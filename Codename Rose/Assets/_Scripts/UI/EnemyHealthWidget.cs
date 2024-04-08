using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.EnemyScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.UI
{
    public class EnemyHealthWidget : HeroHealthWidget
    {
        [SerializeField] private TextMeshProUGUI _text;
        private Animator _animator;
        private string _name;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void ShowHealthBar(string name)
        {
            _name = name;
            _animator.Play("show");
            StartCoroutine(TypeText());
        }

        private void HideHealthBar()
        {
            _animator.Play("hide");
            StartCoroutine(ClearText());
        }

        private IEnumerator ClearText()
        {
            List<char> name = new List<char>();
            foreach (var letter in _name)
            {
                name.Add(letter);
            }

            while (name.Count > 0)
            {
                _text.text = String.Empty;
                name.Remove(name.Last());
                foreach (var letter in name)
                {
                    _text.text += letter;
                }

                yield return new WaitForSeconds(0.09f);
            }
        }

        private IEnumerator TypeText()
        {
            _text.text = String.Empty;
            yield return new WaitForSeconds(0.6f);
            foreach (var letter in _name)
            {
                _text.text += letter;
                yield return new WaitForSeconds(0.09f);
            }
        }

        protected override void OnEnable()
        {
            AngelAI.OnFightStart += ShowHealthBar;
            AngelAI.OnDamage += SetHpBar;
            AngelAI.OnDeath += HideHealthBar;
        }

        protected override void OnDisable()
        {
            AngelAI.OnFightStart -= ShowHealthBar;
            AngelAI.OnFightStart -= ShowHealthBar;
            AngelAI.OnDeath -= HideHealthBar;
        }
    }
}