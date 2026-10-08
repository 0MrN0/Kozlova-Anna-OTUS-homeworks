using Code.Components;
using Code.Configs;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class DamageSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<DamageEvent>> _filter = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Died> _diedPool;
        private readonly EcsPoolInject<DeathTimer> _timerPool;
        private readonly EcsPoolInject<AttackTarget> _targetPool;
        private readonly EcsPoolInject<DeathDelay> _deathDelayPool;

        public void Run(EcsSystems systems)
        {
            var damageEventPool = _filter.Pools.Inc1;
            var deadPool = _deadPool.Value;
            var healthPool = _healthPool.Value;
            var diedPool = _diedPool.Value;
            var timerPool = _timerPool.Value;
            var targetPool = _targetPool.Value;
            var deathDelayPool = _deathDelayPool.Value;

            foreach (var entity in _filter.Value)
            {
                var damageEvent = damageEventPool.Get(entity);

                if (!damageEvent.Target.Unpack(_world.Value, out var target)
                        || deadPool.Has(target)
                        || !healthPool.Has(target))
                {
                    damageEventPool.Del(entity);
                    continue;
                }

                ref var health = ref healthPool.Get(target);
                health.Current -= damageEvent.Amount;
                if (health.Current <= 0)
                {
                    deadPool.Add(target);
                    diedPool.Add(target);

                    if (deathDelayPool.Has(target))
                    {
                        timerPool.Add(target).Value = deathDelayPool.Get(target).Value;
                    }

                    if (targetPool.Has(target))
                    {
                        targetPool.Del(target);
                    }
                }

                damageEventPool.Del(entity);
            }
        }
    }
}