using Code.Gameplay.Flower.View;
using Code.Gameplay.ScoreSystem;

namespace Code.Gameplay.Flower
{
    public sealed class Flower
    {
        private readonly ScoreStorage _scoreStorage;
        private readonly IFlowerView _view;

        private long _scoreValue;

        public Flower(ScoreStorage scoreStorage, IFlowerView view, long scoreValue)
        {
            _scoreStorage = scoreStorage;
            _view = view;
            _scoreValue = scoreValue;

            _view.Pickupped += OnPickup;
        }

        private void OnPickup()
        {
            _view.Dispose();
            _scoreStorage.AddScore(_scoreValue);
        }
    }
}