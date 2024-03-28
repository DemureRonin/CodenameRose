using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.MapGeneration
{
    [CreateAssetMenu(menuName = "Biome")]
    public class Biome : ScriptableObject
    {
        [SerializeField] private string _biomeName;
        [SerializeField] private TileBase _groundTile;

        [Range(1, 10)] [SerializeField] private int _grassScarsity;
        [SerializeField] private GameObject[] _grass;

        [SerializeField] private GameObject[] _collectablePlants;

        [Range(1, 100)] [SerializeField] private int _plantScarsity;
        [SerializeField] private GameObject[] _decorativePlants;

        [SerializeField] private GameObject[] _creatureGroups;

        public GameObject[] CreatureGroups => _creatureGroups;
        [SerializeField] private int _biomeSize;

        public string BiomeName => _biomeName;

        public int BiomeSize => _biomeSize;


        public int GrassScarsity => _grassScarsity;

        public GameObject[] DecorativePlants => _decorativePlants;

        public TileBase GroundTile => _groundTile;

        public GameObject[] Grass => _grass;

        public GameObject[] CollectablePlants => _collectablePlants;
        public int PlantScarsity => _plantScarsity;
    }
}