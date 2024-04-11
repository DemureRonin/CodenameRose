using System;
using UnityEngine;

namespace _Scripts.UI.Model
{
    [CreateAssetMenu(menuName = "OptionButtonDef")]
    public class OptionButtonDef : ScriptableObject
    {
        [SerializeField] private OptionButton[] _buttons;

        public OptionButton[] Buttons => _buttons;
    }

    [Serializable]
    public class OptionButton
    {
        [SerializeField] private ItemTypes _type;
        [SerializeField] private GameObject _buttonGameObject;
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite => _sprite;


        public ItemTypes Type => _type;

        public GameObject ButtonObject => _buttonGameObject;
    }
}