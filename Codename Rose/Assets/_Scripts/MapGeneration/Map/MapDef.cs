using _Scripts.MapGeneration.Biome;

namespace _Scripts.MapGeneration.Map
{
    public static class MapDef
    {
        public static int Seed => GenerateRandomSeed();

        public static readonly int NumOfBiomes = 3;

        public static readonly int SortingDiscretion = 5;

        public static readonly int BiomeSize = 32;
        public static readonly int ChunkSize = 5;
        public static readonly BiomeObject[,] Biomes = new BiomeObject[NumOfBiomes, NumOfBiomes];

        private static int GenerateRandomSeed()
        {
            var rand = new System.Random();
            var seed = rand.Next(0, int.MaxValue);
            return seed;
        }
    }
}