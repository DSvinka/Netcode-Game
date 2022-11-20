using System;
using Code.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenu
{
    public class MainMenuView: MonoBehaviour
    {
        [SerializeField, ShowOnly] private StartGameMenuView _startGameMenuView;
        [SerializeField, ShowOnly] private ConnectMenuView _connectMenuView;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _connectButton;

        public StartGameMenuView StartGameMenuView => _startGameMenuView;
        public ConnectMenuView ConnectMenuView => _connectMenuView;

        private void Awake()
        {
            _startGameMenuView = GetComponentInChildren<StartGameMenuView>();
            _connectMenuView = GetComponentInChildren<ConnectMenuView>();

            ChangeAllMenu(false);
            
            _startGameButton.onClick.AddListener(ShowStartGameMenu);
            _connectButton.onClick.AddListener(ShowConnectMenu);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _connectButton.onClick.RemoveAllListeners();

            ChangeAllMenu(true);
        }

        private void ShowStartGameMenu()
        {
            _startGameMenuView.gameObject.SetActive(true);
            _connectMenuView.gameObject.SetActive(false);
        }
        
        private void ShowConnectMenu()
        {
            _startGameMenuView.gameObject.SetActive(false);
            _connectMenuView.gameObject.SetActive(true);
        }

        private void ChangeAllMenu(bool active)
        {
            _startGameMenuView.gameObject.SetActive(active);
            _connectMenuView.gameObject.SetActive(active);
        }
    }
}