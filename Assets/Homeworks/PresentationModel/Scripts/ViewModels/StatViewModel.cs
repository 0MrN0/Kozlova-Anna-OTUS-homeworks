using System;
using R3;

namespace Lessons.Architecture.PM
{
    public sealed class StatViewModel : IStatViewModel
    {
        public ReadOnlyReactiveProperty<string> StatString => _statString;

        private readonly ReactiveProperty<string> _statString;
        private readonly Stat _stat;
        private readonly StatStringFormat _statStringFormat;
        private readonly IDisposable _disposable;

        public StatViewModel(Stat stat, StatStringFormat statStringFormat)
        {
            _stat = stat;
            _statStringFormat = statStringFormat;
            _statString = new(ComputeStat());
            _disposable = _stat.Value.Subscribe(_ => UpdateStat());
        }

        private void UpdateStat()
        {
            _statString.Value = ComputeStat();
        }

        private string ComputeStat()
        {
            return string.Format(_statStringFormat.StatFormat, _stat.Name, _stat.Value.CurrentValue);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}