using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class LifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Lifetime>, Exc<DestroyRequest>> _filter;
        private readonly EcsPoolInject<DestroyRequest> _destroyRequestPool;

        public void Run(EcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            var lifetimePool = _filter.Pools.Inc1;
            var destroyRequestPool = _destroyRequestPool.Value;

            foreach (var entity in _filter.Value)
            {
                ref var lifetime = ref lifetimePool.Get(entity);
                lifetime.Value -= deltaTime;
                if (lifetime.Value <= 0f)
                {
                    destroyRequestPool.Add(entity);
                }
            }
        }
    }
}