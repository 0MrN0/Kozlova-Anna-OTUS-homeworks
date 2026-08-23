using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class StatViewModelFactory
    {
        private readonly StatStringFormat _statStringFormat;

        [Inject]
        public StatViewModelFactory(StatStringFormat statStringFormat)
        {
            _statStringFormat = statStringFormat;
        }

        public IStatViewModel Create(Stat stat)
        {
            return new StatViewModel(stat, _statStringFormat);
        }
    }
}
