using Code.Core.Contracts;
using Code.Gameplay.Flower.View;
using Code.Infrastructure.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Flower
{
    public sealed class FlowerFactory : IFactory<Vector3, FlowerConfig, Flower>
    {
        private readonly IInstantiator _instantiator;
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public FlowerFactory(IInstantiator instantiator, ISaveLoadAggregate saveLoadAggregate)
        {
            _instantiator = instantiator;
            _saveLoadAggregate = saveLoadAggregate;
        }

        public Flower Create(Vector3 position, FlowerConfig config)
        {
            var view = _instantiator.InstantiatePrefabForComponent<IFlowerView>(config.FlowerPrefab,
                                                                                position,
                                                                                Quaternion.identity,
                                                                                null);
            var flower = _instantiator.Instantiate<Flower>(new object[] { view, config });
            _saveLoadAggregate.Register(flower);

            void OnPickupped(ISaveLoad pickedFlower)
            {
                _saveLoadAggregate.Unregister(pickedFlower);
                flower.Pickupped -= OnPickupped;
            }

            flower.Pickupped += OnPickupped;

            return flower;
        }
    }
}