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
        private readonly EcsFilterInject<Inc<TriggerEnterEvent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<AttackCooldown> _attackCooldownPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsPoolInject<AttackTarget> _attackTargetPool;

        public void Run(EcsSystems systems)
        {
            var triggerEventPool = _filter.Pools.Inc1;
            var teamPool = _teamPool.Value;
            var attackCdPool = _attackCooldownPool.Value;
            var healthPool = _healthPool.Value;
            var deadPool = _deadPool.Value;
            var targetPool = _attackTargetPool.Value;

            foreach (var entity in _filter.Value)
            {
                var triggerEvent = triggerEventPool.Get(entity);

                if (triggerEvent.Owner.Unpack(_world.Value, out var owner)
                        && triggerEvent.Other.Unpack(_world.Value, out var other)
                        && attackCdPool.Has(owner)
                        && healthPool.Has(other)
                        && teamPool.Has(owner)
                        && teamPool.Has(other)
                        && !targetPool.Has(owner)
                        && !deadPool.Has(owner)
                        && !deadPool.Has(other))
                {
                    var ownerTeam = teamPool.Get(owner).Value;
                    var otherTeam = teamPool.Get(other).Value;

                    if (ownerTeam != otherTeam)
                    {
                        targetPool.Add(owner).Value = triggerEvent.Other;
                    }
                }
            }
        }
    }
}