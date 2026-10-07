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
        private readonly EcsFilterInject<Inc<SpawnRequest>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool = EcsWorlds.EVENTS;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        public void Run(EcsSystems systems)
        {
            var requestPool = _filter.Pools.Inc1;
            var teamPool = _teamPool.Value;

            foreach (var entity in _filter.Value)
            {
                var request = requestPool.Get(entity);

                var created = _entityManager.Value.Create(request.Prefab, request.Position, request.Rotation);
                if (teamPool.Has(entity))
                {
                    created.AddData(new Team { Value = teamPool.Get(entity).Value });
                }

                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}