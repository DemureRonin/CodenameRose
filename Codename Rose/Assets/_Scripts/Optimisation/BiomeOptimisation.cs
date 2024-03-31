using _Scripts.UI.World;
using UnityEngine;

namespace _Scripts.Optimisation
{
    public class BiomeOptimisation : MonoBehaviour
    {
        public void LoadBiome(GameObject biome)
        {
            var child = biome.transform.GetChild(0);
            child.gameObject.SetActive(true);
        }

        public void UnloadBiome(GameObject biome)
        {
            var child = biome.transform.GetChild(0);
            child.gameObject.SetActive(false);
        }
    }
}