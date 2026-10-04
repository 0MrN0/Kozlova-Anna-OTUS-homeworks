using Code.Components;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Code.Systems
{
    public sealed class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(EcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;
            var prefabPool = _filter.Pools.Inc4;
            var teamPool = _teamPool.Value;

            foreach (var entity in _filter.Value)
            {
                var position = positionPool.Get(entity).Value;
                var rotation = rotationPool.Get(entity).Value;
                var prefab = prefabPool.Get(entity).Value;

                var created = _entityManager.Value.Create(prefab, position, rotation);
                if (teamPool.Has(entity))
                {
                    created.AddData(new Team { Value = teamPool.Get(entity).Value });
                }

                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}