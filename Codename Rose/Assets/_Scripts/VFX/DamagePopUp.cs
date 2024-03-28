using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Scripts.VFX
{
    public class DamagePopUp : MonoBehaviour
    {
        private GameObject _damagePopUpPrefab;
        private TextMeshPro _text;
        private const string PrefabPath = "DamagePopUp";

        private void Awake()
        {
            _damagePopUpPrefab = (GameObject)Resources.Load(PrefabPath);
            _text = _damagePopUpPrefab.GetComponent<TextMeshPro>();
        }

        public void SpawnDamagePopUp(float damage)
        {
            _text.text = damage.ToString(CultureInfo.InvariantCulture);

            Instantiate(_damagePopUpPrefab, (Vector2)transform.position + Vector2.right/2, Quaternion.identity);
        }
    }
}