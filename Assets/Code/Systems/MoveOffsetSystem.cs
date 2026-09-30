using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class MoveOffsetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, MoveOffset>> _filter;

        public void Run(EcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var offsetPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                ref var offset = ref offsetPool.Get(entity);
                
                position.Value += offset.Value;
                offset.Value = Vector3.zero;
            }
        }
    }
}