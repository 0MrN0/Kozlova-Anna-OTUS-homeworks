using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public interface IPlayerStatsViewModel : IViewModel
    {
        public IReadOnlyList<IStatViewModel> Stats { get; }
    }
}
