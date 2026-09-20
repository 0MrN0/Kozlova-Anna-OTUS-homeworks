using Code.Gameplay.Flower.View;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Flower
{
    public sealed class FlowerFactory : IFactory<Vector3, FlowerConfig, Flower>
    {
        private readonly IInstantiator _instantiator;

        public FlowerFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public Flower Create(Vector3 position, FlowerConfig config)
        {
            var view = _instantiator.InstantiatePrefabForComponent<IFlowerView>(config.FlowerPrefab,
                                                                                position,
                                                                                Quaternion.identity,
                                                                                null);

            return _instantiator.Instantiate<Flower>(new object[] { view, config.ScoreValue });
        }
    }
}