using Code.Core.Data;
using Code.Gameplay.LevelField;
using Code.Infrastructure.SaveLoad;
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
        private readonly IProgressService _progressService;
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public FlowerSpawner(IFactory<Vector3, FlowerConfig, Flower> flowerFactory,
                              GameField gameField,
                              FlowerConfig[] configs,
                              int flowersCount,
                              IProgressService progressService,
                              ISaveLoadAggregate saveLoadAggregate)
        {
            _flowerFactory = flowerFactory;
            _gameField = gameField;
            _configs = configs;
            _flowersCount = flowersCount;
            _progressService = progressService;
            _saveLoadAggregate = saveLoadAggregate;
        }

        public void Initialize()
        {
            if (!_saveLoadAggregate.IsNeedLoadGame)
                GenerateRandomFlowerData();

            foreach (var data in _progressService.Progress.Flowers)
                _flowerFactory.Create(data.Position, FindConfig(data.ConfigId));
        }

        private void GenerateRandomFlowerData()
        {
            for (int i = 0; i < _flowersCount; i++)
            {
                FlowerConfig config = _configs[Random.Range(0, _configs.Length)];
                _progressService.Progress.Flowers.Add(new FlowerProgress
                {
                    ConfigId = config.ConfigId,
                    Position = GetRandomPosition()
                });
            }
        }

        private FlowerConfig FindConfig(string id)
        {
            FlowerConfig config = _configs[0];
            foreach (var c in _configs)
            {
                if (c.ConfigId == id)
                {
                    config = c;
                    break;
                }
            }

            return config;
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
