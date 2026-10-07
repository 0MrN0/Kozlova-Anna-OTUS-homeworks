using UnityEngine;
using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class TargetValidationSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<AttackTarget, Position, VisionRadius>> _filter;
        private readonly EcsPoolInject<SearchTarget> _searchPool;
        private readonly EcsPoolInject<Dead> _deadPool;

        private const float LoseRadiusMult = 1.25f;

        public void Run(EcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var radiusPool = _filter.Pools.Inc3;
            var searchPool = _searchPool.Value;
            var deadPool = _deadPool.Value;

            foreach (var entity in _filter.Value)
            {
                var packedTarget = targetPool.Get(entity);

                if (!packedTarget.Value.Unpack(_world.Value, out var target)
                        || deadPool.Has(target)
                        || !positionPool.Has(target)
                        || !InRadius(positionPool.Get(entity).Value, positionPool.Get(target).Value, radiusPool.Get(entity).Value * LoseRadiusMult))
                {
                    targetPool.Del(entity);
                    searchPool.Add(entity);
                }
            }
        }

        private bool InRadius(Vector3 position1, Vector3 position2, float radius)
        {
            return Vector3.SqrMagnitude(position1 - position2) <= radius * radius;
        }
    }
}