using System;
using _Scripts.MapGeneration.Map;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Widgets.AngelInfo
{
    public class OphanimInfoWidget : MonoBehaviour
    {
        [SerializeField] private AngelWidget[] _angelWidgets;

        private MapState _mapState;

        private void Awake()
        {
            _mapState = FindAnyObjectByType<MapState>();
        }

        private void Start()
        {
            SetInfo(AngelNames.Gabriel);
            SetInfo(AngelNames.Michael);
            SetInfo(AngelNames.Phanuel);
            SetInfo(AngelNames.Raphael);
        }

        private void SetInfo(AngelNames angelNames)
        {
            foreach (var angelWidget in _angelWidgets)
            {
                foreach (var angel in _mapState.Angels.Angels)
                {
                    if (angelWidget.Name == angelNames && angelNames == angel._name)
                    {
                        if (!angel._alive) angelWidget.SetState();
                        if (angel._encountered) angelWidget.SetInfo();
                    }
                }
            }
        }

        private void OnEnable()
        {
            MapState.OnAngelStatusChanged += SetInfo;
        }

        private void OnDisable()
        {
            MapState.OnAngelStatusChanged -= SetInfo;
        }
    }
}