using _Scripts.MapGeneration.Map;
using _Scripts.MapGeneration.ProperNames;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace _Scripts.MapGeneration.Biome
{
    public class BiomeObject : MonoBehaviour
    {
        [SerializeField] private GameObject _biomeObserverPrefab;

        private BoxCollider2D _collider;
        private BiomeDef _biomeType;
        private GameObject _biomeObserver;

        private Vector2 _biomeCenter;

        public Vector2 BiomeCenter => _biomeCenter;
        private int Seed => MapDef.Seed;
        private int BiomeSize => MapDef.BiomeSize;
        private System.Random Random => MapGenerator.Random;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.offset = new Vector2(BiomeSize / 2, BiomeSize / 2);
            _collider.size = new Vector2(BiomeSize - 5, BiomeSize - 5);
        }

        public void GenerateBiome(BiomeDef biomeDef, Tilemap map, int x0, int y0)
        {
            _biomeType = biomeDef;
            int x1 = x0 + BiomeSize;
            int y1 = y0 + BiomeSize;

            for (int i = y0; i < y1; i++)
            {
                for (int j = x0; j < x1; j++)
                {
                    var tilePosition = new Vector3Int(j, i);
                    map.SetTile(tilePosition, biomeDef.GroundTile);
                }
            }


            _biomeCenter = new Vector2((x0 + x1) / 2, (y0 + y1) / 2);
            _biomeObserver = Instantiate(_biomeObserverPrefab, _biomeCenter, Quaternion.identity);
            _biomeObserver.transform.parent = transform;

            GenerateGrass(x0, y0, x1, y1);
            GenerateGrass(x0, y0, x1, y1);
            GenerateGrass(x0, y0, x1, y1);
            GenerateGrass(x0, y0, x1, y1);
            GenerateGrass(x0, y0, x1, y1);
            GenerateDecorativeObjects(x0, y0, x1, y1);
        }

        private void GenerateGrass(int x0, int y0, int x1, int y1)
        {
            var width = y1 - y0;

            var offSetX = Noise.GenerateNoiseMap(width, width, (float)Random.NextDouble());
            var offSetY = Noise.GenerateNoiseMap(width, width, (float)Random.NextDouble());

            int i = 0;
            for (int indexY = y0; indexY < y1; indexY++)
            {
                int j = 0;
                for (int indexX = x0; indexX < x1; indexX++)
                {
                    var position =
                        new Vector2(
                            indexX + offSetX[j, i] * Random.Next(-_biomeType.GrassScarсity, _biomeType.GrassScarсity),
                            indexY + offSetY[j, i] * Random.Next(-_biomeType.GrassScarсity, _biomeType.GrassScarсity));
                    if (position.x > x1 || position.x < x0 || position.y > y1 || position.y < y0)
                    {
                        j++;
                        continue;
                    }

                    var obj = Instantiate(_biomeType.Grass[MapGenerator.Random.Next(0, _biomeType.Grass.Length)],
                        position,
                        Quaternion.identity);
                    obj.transform.parent = _biomeObserver.transform;

                    var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerID = SortingLayer.NameToID("Grass");

                    spriteRenderer.sortingOrder = short.MinValue;

                    j++;
                }

                i++;
            }
        }

        private void GenerateDecorativeObjects(int x0, int y0, int x1, int y1)
        {
            var width = y1 - y0;
            var offSetX = Noise.GenerateNoiseMap(width, width, (float)Random.NextDouble());
            var offSetY = Noise.GenerateNoiseMap(width, width, (float)Random.NextDouble());
            int i = 0;
            for (int indexY = y0; indexY < y1; indexY++)
            {
                int j = 0;
                for (int indexX = x0; indexX < x1; indexX++)
                {
                    var position = new Vector2(
                        indexX + offSetX[j, i] * Random.Next(-_biomeType.DecorativeObjectsScarcity,
                            _biomeType.DecorativeObjectsScarcity),
                        indexY + offSetY[j, i] * Random.Next(-_biomeType.DecorativeObjectsScarcity,
                            _biomeType.DecorativeObjectsScarcity));
                    if (position.x > x1 || position.x < x0 || position.y > y1 || position.y < y0)
                    {
                        j++;
                        continue;
                    }

                    var obj = Instantiate(
                        _biomeType.DecorativeObjects[Random.Next(0, _biomeType.DecorativeObjects.Length)],
                        position, Quaternion.identity);
                    obj.transform.parent = _biomeObserver.transform;
                    j++;
                }

                i++;
            }
        }
    }
}