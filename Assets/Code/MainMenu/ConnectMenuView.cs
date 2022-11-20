using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenu
{
    public class ConnectMenuView: MonoBehaviour
    {
        public event Action<ConnectMenuModel> OnConnectSubmit;

        [SerializeField] private TMP_Text _errorMessage;
        
        [SerializeField] private TMP_InputField _hostInput;
        [SerializeField] private TMP_InputField _nicknameInput;
        [SerializeField] private Button _connectButton;

        private void Start()
        {
            _connectButton.onClick.AddListener(OnConnectButton);
        }

        private void OnDestroy()
        {
            _connectButton.onClick.RemoveAllListeners();
        }

        private void OnConnectButton()
        {
            _errorMessage.text = "";
            
            if (string.IsNullOrEmpty(_hostInput.text))
            {
                _errorMessage.text = "Введите хост! (IP)";
                return;
            }

            if (string.IsNullOrEmpty(_nicknameInput.text))
            {
                _errorMessage.text = "Введите свой никнейм!";
                return;
            }

            var connectMenuModel = new ConnectMenuModel();
            connectMenuModel.Host = _hostInput.text;
            connectMenuModel.Nickname = _nicknameInput.text;
            
            OnConnectSubmit?.Invoke(connectMenuModel);
        }
    }
}