using UnityEngine;
using Code.Components;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class VisionSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsWorldInject _events = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<TriggerEnterEvent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<FireRequest> _attackRequestPool;

        public void Run(EcsSystems systems)
        {
            var triggerEventPool = _filter.Pools.Inc1;
            var teamPool = _teamPool.Value;
            var attackRequestPool = _attackRequestPool.Value;

            foreach (var entity in _filter.Value)
            {
                var triggerEvent = triggerEventPool.Get(entity);

                if (triggerEvent.Owner.Unpack(_world.Value, out var owner)
                        && triggerEvent.Other.Unpack(_world.Value, out var other)
                        && teamPool.Has(owner)
                        && teamPool.Has(other))
                {
                    var ownerTeam = teamPool.Get(owner).Value;
                    var otherTeam = teamPool.Get(other).Value;

                    if (ownerTeam != otherTeam
                            && !attackRequestPool.Has(owner))
                    {
                        attackRequestPool.Add(owner);
                    }
                }

                _events.Value.DelEntity(entity);
            }
        }
    }
}