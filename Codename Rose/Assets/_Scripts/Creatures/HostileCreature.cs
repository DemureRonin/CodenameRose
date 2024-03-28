using System.Collections;
using _Scripts.Creatures.CreatureDef;
using _Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Creatures
{
    public class HostileCreature : Creature
    {
        [SerializeField] private CreatureParameterDef _hostileCreatureParameters;
        [SerializeField] private float _knockBackSpeed;
        [SerializeField] private GameObject _vision;
        [SerializeField] private Transform _projectilePosition;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private AnimationClip _projectileAttackClip;

        private Transform _target;
        private Coroutine _coroutine;
        private WaitForSeconds _attackDelay;
        private Timer _roamingTimer = new(); 

        private bool _isAgro;
        private bool _suddenDamage;
        private bool _attacking;
        private bool _beingDamaged;
        private Vector2 _roamingCentre = default;
        private float _maxRoamingDistance = 5;

        private static readonly int Attack = Animator.StringToHash("projectileAttack");
        private float _enemyProximityThreshold = 2;

        protected override void FixedUpdate()
        {
            if (_beingDamaged) return;
            base.FixedUpdate();
        }

        protected override void Awake()
        {
            InitializeParameters(_hostileCreatureParameters);
            base.Awake();
        }

        private void Start()
        {
            StartState(Roam());
        }
//make general
        public void InitializeGroup(Vector2 roamingCentre, float maxRoamingDistance)
        {
            _maxRoamingDistance = maxRoamingDistance;
            _roamingCentre = roamingCentre;
        }

        private IEnumerator Roam()
        {
            var rand = new System.Random();
            while (!_isAgro)
            {
                var randomAngle = (float)rand.NextDouble() * 360f;

                var randomX = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * _maxRoamingDistance;
                var randomY = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * _maxRoamingDistance;

                var randomPosition = _roamingCentre + new Vector2(randomX, randomY);
                _roamingTimer.StartTimer();
                _roamingTimer.Value = 6;
                while (Vector2.Distance(transform.position, randomPosition) > 0.1f && !_roamingTimer.IsReady)
                {
                    MovementVector = (randomPosition - (Vector2)transform.position).normalized;
                    yield return null;
                }

                MovementVector = Vector2.zero;

                yield return new WaitForSeconds(Random.Range(2f, 5f));
            }
        }

        private void StartState(IEnumerator state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(state);
        }


        public void OnSeeEnemy(GameObject target)
        {
            if (_isAgro) return;
            _isAgro = true;
            _target = target.transform;

            _vision.SetActive(false);
            StartState(MoveToTarget());
            StartCoroutine(AttackChance());
        }

        public void OnSuddenDamage()
        {
            if (_isAgro) return;
            _isAgro = true;
            _target = HealthComponent.Attacker.transform;

            _vision.SetActive(false);
            StartState(MoveToTarget());
            StartCoroutine(AttackChance());
        }

        private IEnumerator AttackChance()
        {
            while (!_attacking)
            {
                var rand = Random.value;
                if (rand > 0.8f)
                {
                    StartState(ProjectileAttack());
                }

                yield return new WaitForSeconds(2f);
            }
        }

        private IEnumerator ProjectileAttack()
        {
            _attacking = true;
            Animator.SetTrigger(Attack);
            MovementVector = Vector2.zero;
            yield return new WaitForSeconds(_projectileAttackClip.length);
            
            Instantiate(_projectilePrefab, _projectilePosition.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            
            _attacking = false;
            StartState(MoveToTarget());
            StartCoroutine(AttackChance());
        }

        private IEnumerator MoveToTarget()
        {
            while (_isAgro)
            {
                if (Vector2.Distance(_target.position, transform.position) > _enemyProximityThreshold)
                    MovementVector = (_target.transform.position - transform.position).normalized;
                else MovementVector = Vector2.zero;

                yield return null;
            }
        }

        public void TakeDamageKnockBack()
        {
            StartCoroutine(TakeKnockBack());
        }

        private IEnumerator TakeKnockBack()
        {
            _beingDamaged = true;
            RigidBody.velocity = Vector2.zero;
            RigidBody.AddForce((transform.position - _target.transform.position).normalized * _knockBackSpeed,
                ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.4f);
            _beingDamaged = false;
        }
    }
}