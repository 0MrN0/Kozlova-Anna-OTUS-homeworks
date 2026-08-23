using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class LevelHelper : MonoBehaviour
    {
        [SerializeField] private LevelView _levelView;

        private LevelViewModelFactory _factory;
        private ILevelViewModel _currentLevelViewModel;
        private PlayerLevel _currentPlayerLevel;

        [Inject]
        public void Construct(LevelViewModelFactory factory)
        {
            _factory = factory;
        }

        public void Show(PlayerLevel playerLevel)
        {
            _currentLevelViewModel?.Dispose();
            _currentPlayerLevel = playerLevel;
            _currentLevelViewModel = _factory.Create(_currentPlayerLevel);
            _levelView.Init(_currentLevelViewModel);
        }

        public void AddExperience(int exp)
        {
            _currentPlayerLevel?.AddExperience(exp);
        }
    }
}