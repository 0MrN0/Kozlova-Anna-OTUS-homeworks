using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsHelper : MonoBehaviour
    {
        [SerializeField] private PlayerStatsView _playerStatsView;

        private StatViewModelFactory _factory;
        private IPlayerStatsViewModel _currentViewModel;
        private PlayerStats _currentPlayerStats;

        [Inject]
        public void Construct(StatViewModelFactory factory)
        {
            _factory = factory;
        }

        public void Show(PlayerStats playerStats)
        {
            _currentViewModel?.Dispose();
            _currentPlayerStats = playerStats;
            _currentViewModel = new PlayerStatsViewModel(playerStats.Stats, _factory);
            _playerStatsView.Init(_currentViewModel);
        }

        public void ChangeStat(string name, int value)
        {
            _currentPlayerStats?.GetStat(name).ChangeValue(value);
        }
    }
}
