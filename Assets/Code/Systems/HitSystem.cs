using Code.Components;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class HitSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsWorldInject _events = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<TriggerEnterEvent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<DestroyRequest> _destroyRequestPool;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool = EcsWorlds.EVENTS;

        public void Run(EcsSystems systems)
        {
            var eventPool = _filter.Pools.Inc1;
            var deadPool = _deadPool.Value;
            var healthPool = _healthPool.Value;
            var damagePool = _damagePool.Value;
            var teamPool = _teamPool.Value;
            var destroyRequestPool = _destroyRequestPool.Value;
            var damageEventPool = _damageEventPool.Value;

            foreach (var entity in _filter.Value)
            {
                var triggerEvent = eventPool.Get(entity);
                if (!triggerEvent.Owner.Unpack(_world.Value, out var bullet)
                        || !triggerEvent.Other.Unpack(_world.Value, out var cube)
                        || !damagePool.Has(bullet)
                        || !healthPool.Has(cube)
                        || !teamPool.Has(bullet)
                        || !teamPool.Has(cube)
                        || deadPool.Has(cube)
                        || destroyRequestPool.Has(bullet))
                {
                    continue;
                }

                var team1 = teamPool.Get(bullet).Value;
                var team2 = teamPool.Get(cube).Value;
                if (team1 == team2) continue;

                var damageEvent = _events.Value.NewEntity();
                damageEventPool.Add(damageEvent) = new DamageEvent
                {
                    Amount = damagePool.Get(bullet).Value,
                    Target = triggerEvent.Other
                };

                destroyRequestPool.Add(bullet);
            }
        }
    }
}