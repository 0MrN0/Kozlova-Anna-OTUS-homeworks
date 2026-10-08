using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<Dead>> _filter;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsPoolInject<Died> _diedPool;
        private readonly EcsPoolInject<DeathDelay> _deathDelayPool;
        private readonly EcsPoolInject<DeathTimer> _timerPool;

        public void Run(EcsSystems systems)
        {
            var healthPool = _filter.Pools.Inc1;
            var deadPool = _deadPool.Value;
            var diedPool = _diedPool.Value;
            var deathDelayPool = _deathDelayPool.Value;
            var timerPool = _timerPool.Value;

            foreach (var entity in _filter.Value)
            {
                if (healthPool.Get(entity).Current <= 0)
                {
                    deadPool.Add(entity);
                    diedPool.Add(entity);

                    if (deathDelayPool.Has(entity))
                    {
                        timerPool.Add(entity).Value = deathDelayPool.Get(entity).Value;
                    }
                }
            }
        }
    }
}