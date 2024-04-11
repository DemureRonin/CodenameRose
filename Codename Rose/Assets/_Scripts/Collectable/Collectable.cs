using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Collectable
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private ItemTypes _id;
        [SerializeField] private Inventory _inventory;
        public delegate void CollectEvent(ItemTypes id);

        public static event CollectEvent OnCollect;

        public void Collect()
        {
            _inventory.AddItem(_id);
            OnCollect?.Invoke(_id);
        }
    }
}