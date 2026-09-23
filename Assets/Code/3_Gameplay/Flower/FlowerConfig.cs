using Code.Gameplay.Flower.View;
using UnityEngine;

namespace Code.Gameplay.Flower
{
    [CreateAssetMenu(menuName = "Configs / Flower Config", fileName = "FlowerConfig")]
    public sealed class FlowerConfig : ScriptableObject
    {
        public long ScoreValue => _scoreValue;
        public FlowerView FlowerPrefab => _flowerPrefab;
        public string ConfigId => _configId;

        [SerializeField] private long _scoreValue = 10;
        [SerializeField] private FlowerView _flowerPrefab;
        [SerializeField] private string _configId;
    }
}