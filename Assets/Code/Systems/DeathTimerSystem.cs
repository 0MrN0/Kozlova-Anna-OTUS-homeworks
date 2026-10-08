using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class DeathTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Dead, DeathTimer>, Exc<DestroyRequest>> _filter;
        private readonly EcsPoolInject<DestroyRequest> _destroyRequestPool;

        public void Run(EcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            var timerPool = _filter.Pools.Inc2;
            var destroyRequestPool = _destroyRequestPool.Value;

            foreach (var entity in _filter.Value)
            {
                ref var timer = ref timerPool.Get(entity);
                timer.Value -= deltaTime;

                if (timer.Value <= 0f)
                    destroyRequestPool.Add(entity);
            }
        }
    }
}