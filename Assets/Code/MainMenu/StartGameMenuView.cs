using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MainMenu
{
    public class StartGameMenuView: MonoBehaviour
    {
        public event Action<StartGameMenuModel> OnStartGameSubmit;

        [SerializeField] private TMP_Text _errorMessage;
        
        [SerializeField] private TMP_InputField _nicknameInput;
        [SerializeField] private Button _startButton;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButton);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveAllListeners();
        }

        private void OnStartButton()
        {
            _errorMessage.text = "";
            
            if (string.IsNullOrEmpty(_nicknameInput.text))
            {
                _errorMessage.text = "Введите свой никнейм!";
                return;
            }

            var model = new StartGameMenuModel();
            model.Nickname = _nicknameInput.text;
            
            OnStartGameSubmit?.Invoke(model);
        }
    }
}