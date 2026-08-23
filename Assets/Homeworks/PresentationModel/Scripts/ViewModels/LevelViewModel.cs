using R3;

namespace Lessons.Architecture.PM
{
    public sealed class LevelViewModel : ILevelViewModel
    {
        public ReadOnlyReactiveProperty<bool> CanLevelUp => _canLevelUp;
        public ReadOnlyReactiveProperty<string> LevelString => _levelString;
        public ReadOnlyReactiveProperty<string> ExperienceString => _experienceString;
        public ReadOnlyReactiveProperty<float> ExperienceProgress => _experienceProgress;

        private readonly ReactiveProperty<bool> _canLevelUp;
        private readonly ReactiveProperty<string> _levelString;
        private readonly ReactiveProperty<string> _experienceString;
        private readonly ReactiveProperty<float> _experienceProgress;

        private readonly PlayerLevel _playerLevel;
        private readonly LevelStringFormat _levelStringFormat;
        private readonly CompositeDisposable _disposable = new();

        public LevelViewModel(PlayerLevel playerLevel, LevelStringFormat levelStringFormat)
        {
            _playerLevel = playerLevel;
            _levelStringFormat = levelStringFormat;

            _canLevelUp = new(ComputeCanLevelUp());
            _levelString = new(ComputeLevelString());
            _experienceString = new(ComputeExperienceString());
            _experienceProgress = new(ComputeExperienceProgress());

            _playerLevel.CurrentLevel
                        .Subscribe(value => OnLevelChanged(value))
                        .AddTo(_disposable);
            _playerLevel.CurrentExperience
                        .Subscribe(value => OnExperienceChanged(value))
                        .AddTo(_disposable);
        }

        public void LevelUp()
        {
            _playerLevel.LevelUp();
        }

        private void OnExperienceChanged(int _)
        {
            _experienceString.Value = ComputeExperienceString();
            _experienceProgress.Value = ComputeExperienceProgress();
            _canLevelUp.Value = ComputeCanLevelUp();
        }

        private void OnLevelChanged(int _)
        {
            _levelString.Value = ComputeLevelString();
            _experienceString.Value = ComputeExperienceString();
            _experienceProgress.Value = ComputeExperienceProgress();
            _canLevelUp.Value = ComputeCanLevelUp();
        }

        private string ComputeExperienceString()
        {
            return string.Format(_levelStringFormat.ExperienceFormat, _playerLevel.CurrentExperience, _playerLevel.RequiredExperience);
        }

        private string ComputeLevelString()
        {
            return string.Format(_levelStringFormat.LevelFormat, _playerLevel.CurrentLevel);
        }

        private float ComputeExperienceProgress()
        {
            return (float)_playerLevel.CurrentExperience.CurrentValue / _playerLevel.RequiredExperience;
        }

        private bool ComputeCanLevelUp()
        {
            return _playerLevel.CanLevelUp();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}