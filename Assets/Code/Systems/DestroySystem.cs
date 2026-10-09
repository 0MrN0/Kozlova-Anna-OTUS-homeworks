using Code.Components;
using Code.Services.Factory;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Code.Systems
{
    public sealed class DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestroyRequest>> _filter;
        private readonly EcsCustomInject<IEntityFactory> _factory;

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _factory.Value.Despawn(entity);
            }
        }
    }
}