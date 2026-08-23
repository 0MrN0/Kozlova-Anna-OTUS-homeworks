using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsHelper : MonoBehaviour
    {
        [SerializeField] private PlayerStatsView _playerStatsView;

        private StatViewModelFactory _factory;
        private IPlayerStatsViewModel _currentViewModel;

        [Inject]
        public void Construct(StatViewModelFactory factory)
        {
            _factory = factory;
        }

        public void Show(PlayerStats playerStats)
        {
            _currentViewModel?.Dispose();
            _currentViewModel = new PlayerStatsViewModel(playerStats.Stats, _factory);
            _playerStatsView.Init(_currentViewModel);
        }
    }
}
