using _Scripts.Components;
using _Scripts.UI.World;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


namespace _Scripts.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _numOfBiomes;
        [SerializeField] private int _biomeSize;
        [SerializeField] private int _chunkSize;
        [SerializeField] private int _seed;
        [SerializeField] private Tilemap _map;
        [SerializeField] private Biome[] _biomes;
        [SerializeField] private RenderSorting _renderSorting;
        [SerializeField] private GameObject _biomeNotifier;

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
            //  var biomes = _mapSize / 16;

            var perlin = Noise.GenerateNoiseMap(_numOfBiomes, _numOfBiomes, (float)_random.NextDouble());
            for (int indexY = 0; indexY < _numOfBiomes; indexY++)
            {
                for (int indexX = 0; indexX < _numOfBiomes; indexX++)
                {
                    var rand = _random.Next(0, _biomes.Length);

                    GenerateBiome(_biomes[rand], indexX, indexY);
                }
            }
        }

        private void GenerateBiome(Biome biome, int x0, int y0)
        {
            y0 *= _biomeSize;
            x0 *= _biomeSize;
            int x1 = x0 + _biomeSize;
            int y1 = y0 + _biomeSize;
            for (int i = y0; i < y1; i++)
            {
                for (int j = x0; j < x1; j++)
                {
                    GenerateChunk(biome, j, i);
                }
            }

            var vector = new Vector2((x0 + x1) / 2, (y0 + y1) / 2)*_chunkSize ;
            var biomeNotifier = Instantiate(_biomeNotifier, vector, Quaternion.identity);
            var biomeNotifie = biomeNotifier.GetComponent<BiomeObserver>();
            biomeNotifie.biomeName = biome.BiomeName;
            biomeNotifier.transform.parent = transform;
        }

        private void GenerateChunk(Biome biome, int x0, int y0)
        {
            y0 *= _chunkSize;
            x0 *= _chunkSize;
            int x1 = x0 + _chunkSize;
            int y1 = y0 + _chunkSize;
            for (int i = y0; i < y1; i++)
            {
                for (int j = x0; j < x1; j++)
                {
                    var vector = new Vector3Int(j, i);
                    _map.SetTile(vector, biome.GroundTile);
                }
            }

            GenerateGrass(biome, x0, y0, x1, y1);
            GeneratePlants(biome, x0, y0, x1, y1);
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
                    var position =
                        new Vector2(indexX + offSetX[j, i] * _random.Next(-biome.GrassScarsity, biome.GrassScarsity),
                            indexY + offSetY[j, i] * _random.Next(-biome.GrassScarsity, biome.GrassScarsity));
                    if (position.x > x1 || position.x < x0 || position.y > y1 || position.y < y0)
                    {
                        j++;
                        continue;
                    }

                    var obj = Instantiate(biome.Grass[_random.Next(0, biome.Grass.Length)], position,
                        Quaternion.identity);


                    var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerID = SortingLayer.NameToID("Grass");

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

                    var obj = Instantiate(biome.DecorativePlants[_random.Next(0, biome.DecorativePlants.Length)],
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