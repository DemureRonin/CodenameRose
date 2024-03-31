using _Scripts.MapGeneration.Biome;

namespace _Scripts.MapGeneration.Map
{
    public static class MapDef
    {
        public static readonly int Seed = 13;

        public static readonly int NumOfBiomes = 8;

        public static readonly int SortingDiscretion = 5;

        public static readonly int BiomeSize = 128;
        public static readonly BiomeObject[,] Biomes = new BiomeObject[NumOfBiomes,NumOfBiomes];
    }
}