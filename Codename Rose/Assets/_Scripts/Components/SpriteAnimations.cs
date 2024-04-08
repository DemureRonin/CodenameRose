using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Scripts.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimations : MonoBehaviour
    {
        [SerializeField][Range(1, 60)] private int _frameRate = 10;
        [SerializeField] private UnityEvent<string> _onComplete;
        [SerializeField] private Clip[] _clips;


        private SpriteRenderer _renderer;

        private float _secondsPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private bool _isPlaying = true;

        private int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondsPerFrame = 1f / _frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaying;
            _isPlaying = false;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            var clip = _clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke(gameObject);
                    _onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }
                }

                return;
            }

            _renderer.sprite = clip.Sprites[_currentFrame];
            _nextFrameTime += _secondsPerFrame;
            _currentFrame++;
        }



    }

    [Serializable]
    class Clip
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _loop; 
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private EnterEvent _onComplete;

        public string Name => _name;

        public bool Loop => _loop;

        public Sprite[] Sprites => _sprites;

        public bool AllowNextClip => _allowNextClip;

        public EnterEvent OnComplete => _onComplete;
    }


}