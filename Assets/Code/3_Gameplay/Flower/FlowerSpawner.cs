using Code.Gameplay.LevelField;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Flower
{
    public sealed class FlowerSpawner : IInitializable
    {
        private readonly IFactory<Vector3, FlowerConfig, Flower> _flowerFactory;
        private readonly GameField _gameField;
        private readonly FlowerConfig[] _configs;
        private readonly int _flowersCount;

        public FlowerSpawner(IFactory<Vector3, FlowerConfig, Flower> flowerFactory,
                              GameField gameField,
                              FlowerConfig[] configs,
                              int flowersCount)
        {
            _flowerFactory = flowerFactory;
            _gameField = gameField;
            _configs = configs;
            _flowersCount = flowersCount;
        }

        public void Initialize()
        {
            for (int i = 0; i < _flowersCount; i++)
            {
                FlowerConfig config = _configs[Random.Range(0, _configs.Length)];

                _flowerFactory.Create(GetRandomPosition(), config);
            }
        }

        private Vector3 GetRandomPosition()
        {
            float halfWidth = _gameField.Width * 0.5f;
            float halfHeight = _gameField.Height * 0.5f;

            float x = Random.Range(-halfWidth, halfWidth);
            float y = Random.Range(-halfHeight, halfHeight);

            return _gameField.transform.position + new Vector3(x, y, 0f);
        }
    }
}
