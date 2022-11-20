using System.Globalization;
using TMPro;
using UnityEngine;

namespace Code.Vehicle
{
    public class VehicleHudView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _playerNameText;

        private void Start()
        {
            if (_healthText == null)
            {
                Debug.LogError($"{name}-{GetInstanceID()} HealthText is Null!");
            }
            
            if (_playerNameText == null)
            {
                Debug.LogError($"{name}-{GetInstanceID()} PlayerNameText is Null!");
            }
        }

        public void SetHealth(float health)
        {
            _healthText.text = health.ToString(CultureInfo.CurrentCulture);
        }
        
        public void SetPlayerName(string playerName)
        {
            _playerNameText.text = playerName;
        }
    }
}