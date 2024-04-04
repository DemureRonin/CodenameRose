using System;
using UnityEngine;

namespace _Scripts.MapGeneration.Biome
{
    [RequireComponent(typeof(BiomeObject))]
    public class BiomeMarkers : MonoBehaviour
    {
        [SerializeField] private Marker _centreMarkerPrefab;
        private BiomeObject _biomeObject;
        private GameObject _marker;

        private void Awake()
        {
            _biomeObject = GetComponent<BiomeObject>();
        }

        public void ShowCentreMarker()
        {
            _marker = Instantiate(_centreMarkerPrefab.gameObject, transform.position, Quaternion.identity);
            var marker = _marker.GetComponent<Marker>();
            marker.SetMarkedLocation(_biomeObject.BiomeCenter);
        }

        public void HideCentreMarker()
        {
            Destroy(_marker);
        }
    }
}