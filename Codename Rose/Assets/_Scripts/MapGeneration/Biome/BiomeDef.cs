using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace _Scripts.MapGeneration.Biome
{
    [CreateAssetMenu(menuName = "Biome")]
    public class BiomeDef : ScriptableObject
    {
        [SerializeField] private string _biomeName;
        [SerializeField] private TileBase _groundTile;

        [FormerlySerializedAs("_grassScarsity")] [Range(1, 10)] [SerializeField] private int _grassScarсity;
        [SerializeField] private GameObject[] _grass;

        [SerializeField] private GameObject[] _collectablePlants;

        [Range(1, 200)] [SerializeField] private int _plantScarsity;
        [SerializeField] private GameObject[] _decorativePlants;

        [SerializeField] private GameObject[] _creatureGroups;
        [SerializeField] private GameObject[] _landMarks;
        [SerializeField] private GameObject _checkpoint;

        public GameObject Checkpoint => _checkpoint;

        public GameObject[] LandMarks => _landMarks;

        public GameObject[] CreatureGroups => _creatureGroups;

        public string BiomeName => _biomeName;

        public int GrassScarсity => _grassScarсity;

        public GameObject[] DecorativePlants => _decorativePlants;

        public TileBase GroundTile => _groundTile;

        public GameObject[] Grass => _grass;

        public GameObject[] CollectablePlants => _collectablePlants;
        public int PlantScarsity => _plantScarsity;
    }
}