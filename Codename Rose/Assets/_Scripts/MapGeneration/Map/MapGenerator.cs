using _Scripts.Components;
using _Scripts.MapGeneration.Biome;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;


namespace _Scripts.MapGeneration.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _map;
        [SerializeField] private BiomeDef[] _biomeDef;
        [SerializeField] private RenderSorting _renderSorting;
        [SerializeField] private BiomeObject _biomeObject;
        [SerializeField] private Transform _spawnableObjectsParent;

        [FormerlySerializedAs("_startVillage")] [SerializeField]
        private GameObject _hero;

        private int Seed => MapDef.Seed;
        private int NumOfBiomes => MapDef.NumOfBiomes;
        private int BiomeSize => MapDef.BiomeSize;


        public static System.Random Random;


        private void Awake()
        {
            Random = new System.Random(Seed);
            GenerateMap();
            _renderSorting.SetSortingLayer();
            UnloadMap();
         
        }

        private void LoadUI()
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }

        private void UnloadMap()
        {
            foreach (var biome in MapDef.Biomes)
            {
                var biomeObserver = biome.transform.GetChild(0);
                biomeObserver.gameObject.SetActive(false);
            }
        }


        [ContextMenu("GenerateMap")]
        private void GenerateMap()
        {
            for (int indexY = 0; indexY < NumOfBiomes; indexY++)
            {
                for (int indexX = 0; indexX < NumOfBiomes; indexX++)
                {
                    var y0 = indexX * BiomeSize;
                    var x0 = indexY * BiomeSize;

                    var biomePosition = new Vector2(x0, y0);
                    var biome = Instantiate(_biomeObject.gameObject, biomePosition, Quaternion.identity);
                    biome.transform.parent = _spawnableObjectsParent.transform;
                    var biomeObject = biome.GetComponent<BiomeObject>();

                    MapDef.Biomes[indexX, indexY] = biomeObject;
                    var center = (NumOfBiomes - 1) / 2;
                    if (indexY == center && indexX == center)
                    {
                        var position = new Vector2((x0 + x0 + BiomeSize) / 2, (y0 + y0 + BiomeSize) / 2);
                        Instantiate(_hero, position, Quaternion.identity);
                    }

                    var rand = Random.Next(0, _biomeDef.Length);
                    biomeObject.GenerateBiome(_biomeDef[rand], _map, x0, y0);
                }
            }
        }
    }
}