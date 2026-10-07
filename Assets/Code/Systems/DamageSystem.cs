using Code.Components;
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
        private readonly EcsPoolInject<DestroyRequest> _destroyRequestPool;

        public void Run(EcsSystems systems)
        {
            var damageEventPool = _filter.Pools.Inc1;
            var deadPool = _deadPool.Value;
            var healthPool = _healthPool.Value;
            var destroyRequestPool = _destroyRequestPool.Value;

            foreach(var entity in _filter.Value)
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
                    destroyRequestPool.Add(target);
                }

                damageEventPool.Del(entity);
            }
        }
    }
}