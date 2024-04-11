using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.UI
{
    [CreateAssetMenu(menuName = "ItemDef")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private Items[] _items;

        public Items[] Items => _items;

        public void AddItem(ItemTypes id)
        {
            foreach (var item in _items)
            {
                if (id == item.ID)
                {
                    item.Obtain();
                }
            }
        }
    }

    [Serializable]
    public class Items
    {
        [SerializeField] private ItemTypes _id;
        [SerializeField] private bool _obtained;

        public bool Obtained => _obtained;

        public ItemTypes ID => _id;

        public void Obtain()
        {
            _obtained = true;
        }
    }

    public enum ItemTypes
    {
        Sword,
        WeatherModule,
        TimeModule,
        CoreReductor,
        ElectricityReductor
    }
}