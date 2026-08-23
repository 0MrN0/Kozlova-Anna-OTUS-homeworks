using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStats
    {
        public IReadOnlyList<Stat> Stats => _stats;

        private readonly List<Stat> _stats;

        public PlayerStats(IReadOnlyList<StatData> statData)
        {
            _stats = new List<Stat>(statData.Count);

            for (var i = 0; i < statData.Count; i++)
            {
                var data = statData[i];
                _stats.Add(new Stat(data.Name, data.Value));
            }
        }

        public Stat GetStat(string name)
        {
            for (var i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].Name == name)
                {
                    return _stats[i];
                }
            }

            throw new ArgumentException($"Stat \"{name}\" is not found!");
        }
    }
}
