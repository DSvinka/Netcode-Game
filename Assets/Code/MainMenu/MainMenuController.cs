using Code.Consts;
using Code.Extensions;
using Code.Singletons;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.MainMenu
{
    public class MainMenuController: MonoBehaviour
    {
        
        [SerializeField, ShowOnly] private MainMenuView _mainMenuView;

        private void Start()
        {
            _mainMenuView = GetComponentInChildren<MainMenuView>();
            
            _mainMenuView.StartGameMenuView.OnStartGameSubmit += OnStartGameSubmit;
            _mainMenuView.ConnectMenuView.OnConnectSubmit += OnConnectSubmit;

            PlayerDataManager.Clear();
        }

        private void OnDestroy()
        {
            _mainMenuView.ConnectMenuView.OnConnectSubmit -= OnConnectSubmit;
            _mainMenuView.StartGameMenuView.OnStartGameSubmit -= OnStartGameSubmit;
        }
        
        private void OnStartGameSubmit(StartGameMenuModel model)
        {
            PlayerDataManager.Health = PlayerSettings.DefaultHealth;
            PlayerDataManager.PlayerName = model.Nickname;
            
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        private void OnConnectSubmit(ConnectMenuModel model)
        {
            PlayerDataManager.Health = PlayerSettings.DefaultHealth;
            PlayerDataManager.PlayerName = model.Nickname;
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = model.Host;
            NetworkManager.Singleton.StartClient();
        }
    }
}