using System;
using Code.Core.Contracts;
using Code.Core.Data;
using Code.Gameplay.Flower.View;
using Code.Gameplay.ScoreSystem;

namespace Code.Gameplay.Flower
{
    public sealed class Flower : ISaveLoad
    {
        public event Action<ISaveLoad> Pickupped;

        private readonly ScoreStorage _scoreStorage;
        private readonly IFlowerView _view;

        private FlowerConfig _config;

        public Flower(ScoreStorage scoreStorage, IFlowerView view, FlowerConfig config)
        {
            _scoreStorage = scoreStorage;
            _view = view;
            _config = config;

            _view.Pickupped += OnPickup;
        }

        public void Load(PlayerProgress progress)
        {
        }

        public void Save(PlayerProgress progress)
        {
            progress.Flowers.Add(new FlowerProgress { ConfigId = _config.ConfigId, Position = _view.Position });
        }

        private void OnPickup()
        {
            _view.Dispose();
            _scoreStorage.AddScore(_config.ScoreValue);
            Pickupped?.Invoke(this);
        }
    }
}