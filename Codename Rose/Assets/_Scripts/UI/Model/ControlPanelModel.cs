using _Scripts.MapGeneration.Map;
using UnityEngine;

namespace _Scripts.UI.Model
{
    public class ControlPanelModel : MonoBehaviour
    {

        public void SetModule(ItemTypes? type)
        {
            switch (type)
            {
                case ItemTypes.TimeModule:
                {
                    MapState.SetNight();
                    break;
                }
                case ItemTypes.WeatherModule:
                {
                    MapState.SetRain();
                    break;
                }
            }
        }

        public void SetReductor(ItemTypes? type)
        {
            Debug.Log("Reductor Set");
        }
    }
}