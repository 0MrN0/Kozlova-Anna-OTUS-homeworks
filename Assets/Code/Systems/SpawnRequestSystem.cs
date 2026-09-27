using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Code.Systems
{
    public sealed class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, PrefabComponent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var position = _filter.Pools.Inc2.Get(entity).Value;
                var rotation = _filter.Pools.Inc3.Get(entity).Value;
                var prefab = _filter.Pools.Inc4.Get(entity).Value;

                _entityManager.Value.Create(prefab, position, rotation);
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}