using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Scripts.VFX
{
    public class DamagePopUp : MonoBehaviour
    {
        [SerializeField] private GameObject _damagePopUpPrefab;
        private TextMeshPro _text;
        private void Awake()
        {
            _text = _damagePopUpPrefab.GetComponent<TextMeshPro>();
        }

        public void SpawnDamagePopUp(float damage)
        {
            _text.text = damage.ToString(CultureInfo.InvariantCulture);
            Instantiate(_damagePopUpPrefab, transform.position, Quaternion.identity);
        }
    }
}
