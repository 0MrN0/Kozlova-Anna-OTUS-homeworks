using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public interface IPlayerStatsViewModel : IViewModel, IDisposable
    {
        public IReadOnlyList<IStatViewModel> Stats { get; }
    }
}
