using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class PositionRestrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, PositionRestrictions>> _filter;

        public void Run(EcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var restrictionsPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                var restrictions = restrictionsPool.Get(entity);

                position.Value = new Vector3(
                    Mathf.Clamp(position.Value.x, restrictions.MinX, restrictions.MaxX),
                    Mathf.Clamp(position.Value.y, restrictions.MinY, restrictions.MaxY),
                    Mathf.Clamp(position.Value.z, restrictions.MinZ, restrictions.MaxZ));

            }
        }
    }
}