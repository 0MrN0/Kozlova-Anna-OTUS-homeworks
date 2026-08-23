using System;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerStats
    {
        public Observable<Stat> OnStatAdded => _onStatAdded;
        public Observable<Stat> OnStatRemoved => _onStatRemoved;

        private readonly Subject<Stat> _onStatAdded = new();
        private readonly Subject<Stat> _onStatRemoved = new();

        private readonly HashSet<Stat> stats = new();

        public void AddStat(Stat stat)
        {
            if (stats.Add(stat))
            {
                _onStatAdded.OnNext(stat);
            }
        }

        public void RemoveStat(Stat stat)
        {
            if (stats.Remove(stat))
            {
                _onStatRemoved.OnNext(stat);
            }
        }

        public Stat GetStat(string name)
        {
            foreach (var stat in stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public Stat[] GetStats()
        {
            return stats.ToArray();
        }
    }
}