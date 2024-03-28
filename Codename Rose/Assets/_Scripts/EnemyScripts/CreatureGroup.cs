using System;
using System.Collections.Generic;
using _Scripts.Creatures;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.EnemyScripts
{
    public class CreatureGroup : MonoBehaviour
    {
        [SerializeField] private GameObject _creature;
        [SerializeField] private int _numToSpawn;
        [SerializeField] private float _roamingBoundsRadius;
        private readonly List<GameObject> _creaturesInGroup = new();

        private void Start()
        {
            _numToSpawn = Random.Range(1, 6);
            SpawnCreatures();
        }

        private void SpawnCreatures()
        {
            for (int i = 0; i < _numToSpawn; i++)
            {
                var position = transform.position;
                var spawnPoint = new Vector2(position.x + i, position.y + i);
                var creature = Instantiate(_creature, spawnPoint, Quaternion.identity);
                _creaturesInGroup.Add(creature);
                creature.transform.parent = transform;
                gameObject.name = _creature.name + " Group";
                var hostile = creature.GetComponent<HostileCreature>();
                hostile.InitializeGroup(transform.position,_roamingBoundsRadius );
            }
        }
        
    }
    
}