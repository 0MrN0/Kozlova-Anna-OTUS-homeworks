using UnityEngine;
using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class FaceMoveDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FaceMoveDirection, MoveDirection, Rotation>, Exc<AttackTarget>> _filter;

        public void Run(EcsSystems systems)
        {
            var directionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                var direction = directionPool.Get(entity);
                direction.Value.y = 0f; // поворот не зависит от выстоты направления
                if (direction.Value.sqrMagnitude < 0.0001f)
                {
                    continue;
                }

                ref var rotation = ref rotationPool.Get(entity);
                rotation.Value = Quaternion.LookRotation(direction.Value);
            }
        }
    }
}