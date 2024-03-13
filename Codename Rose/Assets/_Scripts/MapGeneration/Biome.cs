using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts.MapGeneration
{
    [CreateAssetMenu(menuName = "Biome")]
    public class Biome : ScriptableObject
    {
        [SerializeField] private TileBase _groundTile;
        [SerializeField] private GameObject[] _grass;
        [SerializeField] private GameObject[] _collectablePlants;
        [SerializeField] private GameObject[] _trees;


        [SerializeField] private int _grassPerChunk;
        [SerializeField] private int _collectablePlantsPerChunk;
        [SerializeField] private int _treesPerChunk;

        public int TreesPerChunk => _treesPerChunk;

        public int GrassPerChunk => _grassPerChunk;

        public int CollectablePlantsPerChunk => _collectablePlantsPerChunk;
        public GameObject[] Trees => _trees;

        public TileBase GroundTile => _groundTile;

        public GameObject[] Grass => _grass;

        public GameObject[] CollectablePlants => _collectablePlants;

       
    }
}