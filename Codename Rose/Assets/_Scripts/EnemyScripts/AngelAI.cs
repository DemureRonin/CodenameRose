using System.Collections;
using System.Collections.Generic;
using _Scripts.Components.Health;
using _Scripts.MapGeneration.Map;
using _Scripts.PlayerScripts;
using _Scripts.UI.Widgets.AngelInfo;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace _Scripts.EnemyScripts
{
    public class AngelAI : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private AngelNames _type;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private GameObject _eyeParticle;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _firstAttackDelay;
        [SerializeField] private HealthComponent _healthComponent;

        private float _eyeBlinkTime = 0.3f;
        private Animator _animator;
        private IEnumerator _currentCoroutine;
        private Transform _hero;
        private List<IEnumerator> _attacks;
        private readonly System.Random _random = new();
        private bool _isHeroInVision;
        private CameraFollow _camera;

        public delegate void EnemyStartEvent(string  name);
        public static event EnemyStartEvent OnFightStart;

        public delegate void EnemyEvent();
        public static event EnemyEvent OnDeath;
        public delegate void EncounterEvent(AngelNames name);
        public static event EncounterEvent OnEncounter;
        public static event EncounterEvent OnAngelDeath;

        public delegate void EnemyDamageEvent(float currentHp, float maxHp);
        public static event EnemyDamageEvent OnDamage;
        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _attacks = new List<IEnumerator>()
            {
                Attack1(),
                Attack2(),
                Attack3()
            };
        }

        public void OnTakeDamage()
        {
            OnDamage?.Invoke(_healthComponent.Health, _healthComponent.MaxHp);
        }

        public void OnDie()
        {
            OnAngelDeath?.Invoke(_type);
            OnDeath?.Invoke();
        }

        private void EnterState(IEnumerator coroutine)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = coroutine;
            StartCoroutine(_currentCoroutine);
        }

        public void OnHeroInVision()
        {
            if (_isHeroInVision) return;
            _isHeroInVision = true;
            OnEncounter?.Invoke(_type);
            OnFightStart?.Invoke(_name);
            
            _healthComponent.gameObject.SetActive(true);
            _animator.Play("awake");
            _hero = FindObjectOfType<Hero>().transform;
            AttachCamera();
            ChooseAttack();
        }

        private void AttachCamera()
        {
            _camera  = FindObjectOfType<CameraFollow>();
            _camera.enabled = false;
            StartCoroutine(MoveCameraToCentre());
        }

        private IEnumerator MoveCameraToCentre()
        {
            var targetPosition = transform.position;
            while ((Vector2)_camera.transform.position != (Vector2)targetPosition)
            {
                var pos = _camera.transform.position = Vector2.Lerp((Vector2)_camera.transform.position, (Vector2)targetPosition, 0.007f );

                _camera.transform.position = new Vector3(pos.x, pos.y, -10);
                yield return null;
            }
        }

        public void ReleaseCamera()
        {
            StopCoroutine(MoveCameraToCentre());
            _camera.enabled = true;
        }

        private void ChooseAttack()
        {
            var attacks = new[]
            {
                Attack1(),
                Attack2(),
                Attack3()
            };
            var num = Random.Range(0, _attacks.Count);
            EnterState(attacks[num]);
        }

        private IEnumerator Attack1()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 3; i++)
            {
                SpawnEyeBlink();
                yield return new WaitForSeconds(_eyeBlinkTime);
                ProjectileAttack();
                yield return new WaitForSeconds(0.2f);
            }

            ChooseAttack();
        }

        private IEnumerator Attack2()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < 3; i++)
            {
                SpawnEyeBlink();
                yield return new WaitForSeconds(_eyeBlinkTime);
                RoundProjectileAttack();
                yield return new WaitForSeconds(0.2f);
            }

            ChooseAttack();
        }

        private IEnumerator Attack3()
        {
            yield return new WaitForSeconds(1);
            SpawnEyeBlink();
            yield return new WaitForSeconds(_eyeBlinkTime);
            for (int i = 0; i < 10; i++)
            {
                SprayAttack();
                yield return new WaitForSeconds(0.1f);
            }

            ChooseAttack();
        }

        private void SpawnEyeBlink()
        {
            Instantiate(_eyeParticle, transform.position, Quaternion.identity);
        }

        private void RoundProjectileAttack()
        {
            var points = GenerateCirclePoints(12, 0.1f);
            foreach (var point in points)
            {
                var position = transform.position;
                var projectile = Instantiate(_projectile, point + (Vector2)position, quaternion.identity);
                projectile.GetComponent<Projectile>()
                    .SetDirection((projectile.transform.position - position).normalized);
            }
        }

        private void ProjectileAttack()
        {
            Vector2[] spherePositions =
            {
                Vector2.left,
                Vector2.right,
                Vector2.up,
                Vector2.down,
                new(1, 1),
                new(-1, 1),
                new(1, -1),
                new(-1, -1),
                Vector2.zero
            };
            foreach (var position in spherePositions)
            {
               var pos = position *0.5f;
                var projectile = Instantiate(_projectile, (Vector2)transform.position + pos, quaternion.identity);
                if (position == Vector2.zero)
                {
                    projectile.GetComponent<Projectile>()
                        .SetDirection((_hero.position - projectile.transform.position).normalized);
                    return;
                }


                projectile.GetComponent<Projectile>()
                    .SetDirection((projectile.transform.position - transform.position).normalized);
            }
        }

        private void SprayAttack()
        {
            float spreadRange = 3;
            var direction = (Vector2)_hero.position - (Vector2)transform.position +
                            Random.insideUnitCircle * spreadRange;
            var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            projectile.GetComponent<Projectile>()
                .SetDirection((direction).normalized);
        }

        List<Vector2> GenerateCirclePoints(int numPoints, float radius)
        {
            List<Vector2> points = new List<Vector2>();
            float angleIncrement = 360f / numPoints;

            for (int i = 0; i < numPoints; i++)
            {
                float angle = i * angleIncrement;
                float x = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
                points.Add(new Vector2(x, y));
            }

            return points;
        }
    }
}