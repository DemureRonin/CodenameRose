using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace _Scripts.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _mapSize;
        [SerializeField] private int _chunkSize;
        [SerializeField] private Tilemap _map;
        [SerializeField] private Biome[] _biomes;
        
        private static readonly int Int16MinValue = -32768;
        public static readonly int SortingDiscretion = 4;

        private void Start()
        {
            GenerateMap();
        }

        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            var chunks = _mapSize / _chunkSize;
            for (int indexY = 0; indexY < chunks; indexY++)
            {
                for (int indexX = 0; indexX < chunks; indexX++)
                {
                    GenerateChunk(indexX, indexY);
                }
            }
        }

        private void GenerateChunk(int x0, int y0)
        {
            y0 *= _chunkSize;
            x0 *= _chunkSize;
            int x1 = x0 + _chunkSize;
            int y1 = y0 + _chunkSize;
            var biome = _biomes[Random.Range(0, _biomes.Length)];
            for (int i = y0; i < y1; i++)
            {
                for (int j = x0; j < x1; j++)
                {
                    var vector = new Vector3Int(i, j, 0);
                    _map.SetTile(vector, biome.GroundTile);
                }
            }

            GenerateCollectablePant(x0, y0, x1, y1, biome);
            GenerateGrass(x0, y0, x1, y1, biome);
            GenerateTrees(x0, y0, x1, y1, biome);
        }

        private void GenerateTrees(int x0, int y0, int x1, int y1, Biome biome)
        {
            for (int i = 0; i < biome.TreesPerChunk; i++)
            {
                var position = GetVector(x0, y0, x1, y1);
                var obj = Instantiate(biome.Trees[Random.Range(0, biome.Trees.Length)], position, Quaternion.identity);
                
                var renderer = obj.GetComponent<SpriteRenderer>();
                renderer.sortingOrder = -(int)(obj.transform.position.y*SortingDiscretion);
            }
        }

        private void GenerateGrass(int x0, int y0, int x1, int y1, Biome biome)
        {
            for (int i = 0; i < biome.GrassPerChunk; i++)
            {
                var position = GetVector(x0, y0, x1, y1);
                var obj = Instantiate(biome.Grass[Random.Range(0, biome.Grass.Length)], position, Quaternion.identity);
                var renderer = obj.GetComponent<SpriteRenderer>();
                renderer.sortingOrder = Int16MinValue;
            }
        }

        private void GenerateCollectablePant(int x0, int y0, int x1, int y1, Biome biome)
        {
            for (int i = 0; i < biome.CollectablePlantsPerChunk; i++)
            {
                var position = GetVector(x0, y0, x1, y1);
                var obj = Instantiate(biome.CollectablePlants[Random.Range(0, biome.CollectablePlants.Length)],
                    position,
                    Quaternion.identity);
                var renderer = obj.GetComponent<SpriteRenderer>();
                renderer.sortingOrder = -(int)(obj.transform.position.y*SortingDiscretion);
            }
        }

        private Vector3 GetVector(int x0, int y0, int x1, int y1)
        {
            var offsetX = Random.Range(x0, (float)x1);
            var offsetY = Random.Range(y0, (float)y1);
            var position = new Vector3(offsetX, offsetY, 0);
            return position;
        }
    }
}