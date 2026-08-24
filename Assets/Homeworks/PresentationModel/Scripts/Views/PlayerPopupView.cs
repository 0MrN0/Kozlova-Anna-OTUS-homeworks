using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupView : MonoBehaviour, IPopupView
    {
        [SerializeField] private UserView _userView;
        [SerializeField] private PlayerStatsView _statsView;
        [SerializeField] private LevelView _levelView;

        [Space]
        [SerializeField] private Button _closeButton;

        private IPlayerPopupViewModel _playerPopupViewModel;

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerPopupViewModel playerPopupViewModel)
            {
                Debug.LogError($"PlayerPopupView.Show: viewModel is not IPlayerPopupViewModel. ViewModel's type is: {viewModel.GetType()}");
                return;
            }

            if (_playerPopupViewModel != null)
            {
                Hide();
            }

            _playerPopupViewModel = playerPopupViewModel;
            _userView.Init(_playerPopupViewModel.UserViewModel);
            _statsView.Init(_playerPopupViewModel.StatsViewModel);
            _levelView.Init(_playerPopupViewModel.LevelViewModel);
            gameObject.SetActive(true);
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
        }

        private void Hide()
        {
            _playerPopupViewModel = null;
            _userView.Dispose();
            _statsView.Dispose();
            _levelView.Dispose();
            gameObject.SetActive(false);
        }
    }
}