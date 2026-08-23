using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class LevelViewModelFactory
    {
        private readonly LevelStringFormat _levelStringFormat;

        [Inject]
        public LevelViewModelFactory(LevelStringFormat levelStringFormat)
        {
            _levelStringFormat = levelStringFormat;
        }

        public ILevelViewModel Create(PlayerLevel playerLevel)
        {
            return new LevelViewModel(playerLevel, _levelStringFormat);
        }
    }
}