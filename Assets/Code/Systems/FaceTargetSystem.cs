using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class FaceTargetSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<AttackTarget, Position, Rotation>> _filter;

        public void Run(EcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                var packedTarget = targetPool.Get(entity).Value;
                if (!packedTarget.Unpack(_world.Value, out var target)) continue;
                if (!positionPool.Has(target)) continue;

                var direction = positionPool.Get(target).Value - positionPool.Get(entity).Value;
            
                direction.y = 0f;
                if (direction.sqrMagnitude < 0.0001f)
                {
                    continue;
                }

                ref var rotation = ref rotationPool.Get(entity);
                rotation.Value = Quaternion.LookRotation(direction);
            }
        }
    }
}