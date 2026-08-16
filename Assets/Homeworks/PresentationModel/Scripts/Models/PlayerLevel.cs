using System;
using R3;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevel
    {
        public ReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
        public ReadOnlyReactiveProperty<int> CurrentExperience => _currentExperience;
        public int RequiredExperience => 100 * (_currentLevel.Value + 1);

        private readonly ReactiveProperty<int> _currentLevel;
        private readonly ReactiveProperty<int> _currentExperience;

        public PlayerLevel(int currentExperience, int currentLevel = 1)
        {
            _currentExperience = new(currentExperience);
            _currentLevel = new(currentLevel);
        }

        public void AddExperience(int range)
        {
            var xp = Math.Min(_currentExperience.Value + range, RequiredExperience);
            _currentExperience.Value = xp;
        }

        public void LevelUp()
        {
            if (CanLevelUp())
            {
                _currentExperience.Value = 0;
                _currentLevel.Value++;
            }
        }

        public bool CanLevelUp()
        {
            return _currentExperience.Value == RequiredExperience;
        }
    }
}