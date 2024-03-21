using _Scripts.Components;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


namespace _Scripts.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _mapSize;
        [SerializeField] private int _chunkSize;
        [SerializeField] private int _seed;
        [SerializeField] private Tilemap _map;
        [SerializeField] private Biome[] _biomes;
        [SerializeField] private RenderSorting _renderSorting;

        private static readonly int Int16MinValue = -32768;
        public static readonly int SortingDiscretion = 4;

        private System.Random _random;

        private void Start()
        {
            _random = new System.Random(_seed);
            GenerateMap();
            _renderSorting.SetSortingLayer();
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
                    var vector = new Vector3Int(i, j);
                    _map.SetTile(vector, biome.GroundTile);
                }
            }

            GeneratePlants(_biomes[0], x0, y0, x1, y1);
            GenerateGrass(_biomes[0], x0, y0, x1, y1);
        }

        private void GenerateGrass(Biome biome, int x0, int y0, int x1, int y1)
        {
            var width = y1 - y0;


            var noiseMap = Noise.GenerateNoiseMap(width, width, (float)_random.NextDouble());
            var offSetX = Noise.GenerateNoiseMap(width, width, (float)_random.NextDouble());
            var offSetY = Noise.GenerateNoiseMap(width, width, (float)_random.NextDouble());

            int i = 0;
            for (int indexY = y0; indexY < y1; indexY++)
            {
                int j = 0;
                for (int indexX = x0; indexX < x1; indexX++)
                {
                    var position = new Vector2(
                        indexX + offSetX[j, i] * _random.Next(-biome.GrassScarsity, biome.GrassScarsity),
                        indexY + offSetY[j, i] * _random.Next(-biome.GrassScarsity, biome.GrassScarsity));
                    if (position.x > x1 || position.x < x0 || position.y > y1 || position.y < y0)
                    {
                        j++;
                        continue;
                    }

                    var obj = Instantiate(biome.Grass[ _random.Next(0,biome.Grass.Length)], position, Quaternion.identity);
                   

                    var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerID = SortingLayer.NameToID("Grass");;
                    spriteRenderer.sortingOrder = Int16MinValue;
                    j++;
                }

                i++;
            }
        }

        private void GeneratePlants(Biome biome, int x0, int y0, int x1, int y1)
        {
            var width = y1 - y0;
            var offSetX = Noise.GenerateNoiseMap(width, width, (float)_random.NextDouble());
            var offSetY = Noise.GenerateNoiseMap(width, width, (float)_random.NextDouble());
            int i = 0;
            for (int indexY = y0; indexY < y1; indexY++)
            {
                int j = 0;
                for (int indexX = x0; indexX < x1; indexX++)
                {
                    var position = new Vector2(
                        indexX + offSetX[j, i] * _random.Next(-biome.PlantScarsity, biome.PlantScarsity),
                        indexY + offSetY[j, i] * _random.Next(-biome.PlantScarsity, biome.PlantScarsity));
                    if (position.x > x1 || position.x < x0 || position.y > y1 || position.y < y0)
                    {
                        j++;
                        continue;
                    }

                    var obj = Instantiate(biome.DecorativePlants[_random.Next(0,biome.DecorativePlants.Length)],
                        position, Quaternion.identity);

                   // var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    //spriteRenderer.sortingOrder = Int16MinValue + 1;
                    j++;
                }

                i++;
            }
        }
    }
}