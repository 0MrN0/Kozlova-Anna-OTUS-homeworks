using System;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterInfo
    {
        public Observable<CharacterStat> OnStatAdded => _onStatAdded;
        public Observable<CharacterStat> OnStatRemoved => _onStatRemoved;

        private readonly Subject<CharacterStat> _onStatAdded;
        private readonly Subject<CharacterStat> _onStatRemoved;

        private readonly HashSet<CharacterStat> stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (stats.Add(stat))
            {
                _onStatAdded.OnNext(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (stats.Remove(stat))
            {
                _onStatRemoved.OnNext(stat);
            }
        }

        public CharacterStat GetStat(string name)
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

        public CharacterStat[] GetStats()
        {
            return stats.ToArray();
        }
    }
}