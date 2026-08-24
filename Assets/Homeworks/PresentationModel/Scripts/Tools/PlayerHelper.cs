using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerHelper : MonoBehaviour
    {
        [SerializeField] private PlayerPopupView _playerPopupView;
        [SerializeField] private PlayerData _playerData;

        private LevelViewModelFactory _levelFactory;
        private StatViewModelFactory _statFactory;
        private UserViewModelFactory _userInfoFactory;

        private IPlayerPopupViewModel _currentViewModel;
        private PlayerLevel _currentLevelData;
        private UserInfo _currentUserInfo;
        private PlayerStats _currentStats;

        [Inject]
        public void Construct(LevelViewModelFactory levelFactory, StatViewModelFactory statFactory, UserViewModelFactory userInfoFactory)
        {
            _levelFactory = levelFactory;
            _statFactory = statFactory;
            _userInfoFactory = userInfoFactory;
        }
        
        public void Show()
        {
            _currentViewModel?.Dispose();

            _currentLevelData = new(_playerData.CurrentExperience, _playerData.CurrentLevel);
            _currentUserInfo = new(_playerData.Name, _playerData.Description, _playerData.Icon);
            _currentStats = new(_playerData.Stats);

            _currentViewModel = new PlayerPopupViewModel(_userInfoFactory.Create(_currentUserInfo),
                                                         new PlayerStatsViewModel(_currentStats.Stats, _statFactory),
                                                         _levelFactory.Create(_currentLevelData));
            
            _playerPopupView.Show(_currentViewModel);
        }

        public void ChangeStat(string name, int value)
        {
            _currentStats?.GetStat(name).ChangeValue(value);
        }

        public void ChangeName(string name)
        {
            _currentUserInfo?.ChangeName(name);
        }

        public void ChangeDescription(string description)
        {
            _currentUserInfo?.ChangeDescription(description);
        }

        public void AddExperience(int exp)
        {
            _currentLevelData?.AddExperience(exp);
        }
    }
}