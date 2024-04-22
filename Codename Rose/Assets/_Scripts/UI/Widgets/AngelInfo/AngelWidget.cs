using System;
using _Scripts.MapGeneration.Map;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Widgets.AngelInfo
{
    public class AngelWidget : MonoBehaviour
    {
        [SerializeField] private AngelNames _name;


        [SerializeField] private TextMeshProUGUI _status;
        [SerializeField] private TextMeshProUGUI _info;
        [SerializeField] private TextMeshProUGUI _blocker;
        public AngelNames Name => _name;

        public void SetState()
        {
            _status.text = "Status: Dead";
        }

        public void SetInfo()
        {
            _info.gameObject.SetActive(true);
            _blocker.gameObject.SetActive(false);
        }
    }
}