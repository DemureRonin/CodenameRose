using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.MapGeneration.Biome
{
    [CreateAssetMenu(menuName = "Biome")]
    public class BiomeDef : ScriptableObject
    {
        [SerializeField] private string _biomeName;
        [SerializeField] private TileBase _groundTile;

        [Range(1, 10)] [SerializeField] private int _grassScarсity;
        [SerializeField] private GameObject[] _grass;

        [Range(1, 500)] [SerializeField] private int _decorativeObjectsScarcity;
        [SerializeField] private GameObject[] _decorativeObjects;
        
        public int DecorativeObjectsScarcity => _decorativeObjectsScarcity;

        public string BiomeName => _biomeName;

        public int GrassScarсity => _grassScarсity;

        public GameObject[] DecorativeObjects => _decorativeObjects;

        public TileBase GroundTile => _groundTile;

        public GameObject[] Grass => _grass;
    }
}