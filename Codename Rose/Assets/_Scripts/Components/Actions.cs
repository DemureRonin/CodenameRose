using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components
{
    [Serializable]
    public class Actions
    {
        [SerializeField] private string _otherTag = "Player";

        [SerializeField] private EnterEvent _gameEvent;
        public string OtherTag => _otherTag;

        public EnterEvent GameEvent => _gameEvent;
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}