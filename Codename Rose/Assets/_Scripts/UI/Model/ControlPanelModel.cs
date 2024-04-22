using _Scripts.EnemyScripts;
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

        public void SetReductor(ItemTypes? type, int lane)
        {
            var angels = FindObjectsByType<AngelProperties>(FindObjectsSortMode.None);
            switch (type)
            {
                case ItemTypes.CoreReductor:
                {
                    foreach (var angel in angels)
                    {
                        if (angel.Lane == lane)
                            angel.CanTransmitToCore = false;
                    }
                    break;
                }
                case ItemTypes.ElectricityReductor:
                {
                    foreach (var angel in angels)
                    {
                        if (angel.Lane != lane) continue;
                        angel.CanTransmitToCore = false;
                        angel.SelfDestruct();

                    }
                    break;
                }
            }
           
        }
    }
}