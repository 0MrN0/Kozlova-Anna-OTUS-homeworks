using System;

namespace Code.Gameplay.ScoreSystem
{
    public sealed class ScoreStorage
    {
        public event Action<long> ScoreChanged;
        public long Score { get; private set; }

        public ScoreStorage(long score)
        {
            Score = score;
        }

        public void AddScore(long score)
        {
            Score += score;
            ScoreChanged?.Invoke(Score);
        }
    }
}