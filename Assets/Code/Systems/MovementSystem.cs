using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, MoveDirection, MoveSpeed>> _filter;
        private readonly EcsPoolInject<MoveOffset> _offsetPool;

        public void Run(EcsSystems systems)
        {
            var deltaTime = Time.deltaTime;

            var positionPool = _filter.Pools.Inc1;
            var moveDirectionPool = _filter.Pools.Inc2;
            var moveSpeedPool = _filter.Pools.Inc3;


            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                var moveDirection = moveDirectionPool.Get(entity);
                var moveSpeed = moveSpeedPool.Get(entity);

                position.Value += moveDirection.Value * (moveSpeed.Value * deltaTime);

                if (_offsetPool.Value.Has(entity))
                {
                    ref var offset = ref _offsetPool.Value.Get(entity);
                    position.Value += offset.Value;
                    offset.Value = Vector3.zero;
                }
            }
        }
    }
}