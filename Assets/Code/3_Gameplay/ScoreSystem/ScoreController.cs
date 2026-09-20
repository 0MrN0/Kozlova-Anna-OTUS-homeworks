using System;

namespace Code.Gameplay.ScoreSystem
{
    public sealed class ScoreController : IDisposable
    {
        private readonly ScoreStorage _storage;
        private readonly ScoreView _view;

        public ScoreController(ScoreStorage storage, ScoreView view)
        {
            _storage = storage;
            _view = view;

            _view.SetupScore(_storage.Score);
            _storage.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(long score)
        {
            _view.SetupScoreWithAnimation(score);
        }

        public void Dispose()
        {
            _storage.ScoreChanged -= OnScoreChanged;
        }
    }
}