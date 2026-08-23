using UnityEngine;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsHelper : MonoBehaviour
    {
        [SerializeField] private StatView _moveSpeedStatView;
        [SerializeField] private StatStringFormat _statStringFormat;

        private Stat _currentStat;
        private IStatViewModel _currentViewModel;


        public void ShowFirstStat(PlayerData playerData)
        {
            _currentViewModel?.Dispose();
            _currentStat = new(playerData.Stats[0].Name, playerData.Stats[0].Value);
            _currentViewModel = new StatViewModel(_currentStat, _statStringFormat);
            _moveSpeedStatView.Init(_currentViewModel);
        }

        public void ChangeFirstStatValue(int value)
        {
            _currentStat?.ChangeValue(value);
        }
    }
}