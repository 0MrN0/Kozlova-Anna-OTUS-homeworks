using System;
using Code.Core.Contracts;
using Code.Core.Data;
using Code.Infrastructure.SaveLoad;

namespace Code.Gameplay.ScoreSystem
{
    public sealed class ScoreStorage : ISaveLoad
    {
        public event Action<long> ScoreChanged;
        public long Score { get; private set; }

        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public ScoreStorage(ISaveLoadAggregate saveLoadAggregate)
        {
            _saveLoadAggregate = saveLoadAggregate;
            _saveLoadAggregate.Register(this);
        }

        public void AddScore(long score)
        {
            Score += score;
            ScoreChanged?.Invoke(Score);
        }

        public void Save(PlayerProgress progress)
        {
            progress.Score = Score;
        }

        public void Load(PlayerProgress progress)
        {
            Score = progress.Score;
            ScoreChanged?.Invoke(Score);
        }
    }
}