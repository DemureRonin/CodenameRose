using UnityEngine;

namespace _Scripts.UI.World
{
    public class BiomeObserver : MonoBehaviour
    {
        public string biomeName; 
        public delegate void ExploreEvent(string biomeName);

        public static event ExploreEvent OnExplore;

        public void OnBiomeExplored()
        {
            OnExplore?.Invoke(biomeName);
        }
    }
}