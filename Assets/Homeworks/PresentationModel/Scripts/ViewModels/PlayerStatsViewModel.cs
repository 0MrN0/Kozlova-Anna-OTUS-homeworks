using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStatsViewModel : IPlayerStatsViewModel
    {
        public IReadOnlyList<IStatViewModel> Stats => _stats;

        private readonly List<IStatViewModel> _stats;

        public PlayerStatsViewModel(IReadOnlyList<Stat> stats, StatViewModelFactory factory)
        {
            _stats = new List<IStatViewModel>(stats.Count);

            for (var i = 0; i < stats.Count; i++)
            {
                _stats.Add(factory.Create(stats[i]));
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _stats.Count; i++)
            {
                _stats[i].Dispose();
            }
        }
    }
}
