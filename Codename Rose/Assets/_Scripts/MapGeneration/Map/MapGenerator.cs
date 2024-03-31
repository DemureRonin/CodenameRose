using _Scripts.Components;
using _Scripts.MapGeneration.Biome;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace _Scripts.MapGeneration.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _map;
        [SerializeField] private BiomeDef[] _biomeDef;
        [SerializeField] private RenderSorting _renderSorting;
        [SerializeField] private BiomeObject _biomeObject;
        [SerializeField] private Transform _spawnableOnjectsParent;
        private int Seed => MapDef.Seed;
        private int NumOfBiomes => MapDef.NumOfBiomes;
        private int BiomeSize => MapDef.BiomeSize;


        public static System.Random Random;


        private void Start()
        {
            Random = new System.Random(Seed);
            GenerateMap();
            _renderSorting.SetSortingLayer();
        }


        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            for (int indexY = 0; indexY < NumOfBiomes; indexY++)
            {
                for (int indexX = 0; indexX < NumOfBiomes; indexX++)
                {
                    var rand = Random.Next(0, _biomeDef.Length);

                    var y0 = indexX * BiomeSize;
                    var x0 = indexY * BiomeSize;

                    var biomePosition = new Vector2(x0, y0);
                    var biome = Instantiate(_biomeObject.gameObject, biomePosition, Quaternion.identity);
                    biome.transform.parent = _spawnableOnjectsParent.transform;
                    var biomeObject = biome.GetComponent<BiomeObject>();

                    MapDef.Biomes[indexX, indexY] = biomeObject;
                    biomeObject.GenerateBiome(_biomeDef[rand], _map, x0, y0);
                }
            }
        }
    }
}