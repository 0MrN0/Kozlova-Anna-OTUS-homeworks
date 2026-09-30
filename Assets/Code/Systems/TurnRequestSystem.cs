using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class TurnRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TurnRequest, TurnAngle, MoveDirection>> _filter;

        public void Run(EcsSystems systems)
        {
            var requestPool = _filter.Pools.Inc1;
            var turnAnglePool = _filter.Pools.Inc2;
            var moveDirectionPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                var turn = Quaternion.Euler(0f, turnAnglePool.Get(entity).Value, 0f);
                ref var direction = ref moveDirectionPool.Get(entity);

                direction.Value = turn * direction.Value;

                requestPool.Del(entity);
            }
        }
    }
}