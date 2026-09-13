using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.GameModes.ModeViews
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;

        private IGameStartRequester _gameStartRequester;

        [Inject]
        public void Construct(IGameStartRequester newGameRequester)
        {
            _gameStartRequester = newGameRequester;
        }

        private void Start()
        {
            _newGameButton.onClick.AddListener(RequestNewGame);
            _loadGameButton.onClick.AddListener(RequestLoadGame);
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveListener(RequestNewGame);
            _loadGameButton.onClick.RemoveListener(RequestLoadGame);
        }

        private void RequestLoadGame()
        {
            _gameStartRequester.RequestLoadGame();
        }

        private void RequestNewGame()
        {
            _gameStartRequester.RequestNewGame();
        }
    }
}