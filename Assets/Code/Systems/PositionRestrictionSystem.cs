using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class PositionRestrictionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, PositionRestrictions>> _filter;
        private readonly EcsPoolInject<TurnRequest> _turnRequestPool;
        private readonly EcsPoolInject<TurnAngle> _turnAnglePool;

        public void Run(EcsSystems systems)
        {
            var positionPool = _filter.Pools.Inc1;
            var restrictionsPool = _filter.Pools.Inc2;
            var turnRequestPool = _turnRequestPool.Value;
            var turnAnglePool = _turnAnglePool.Value;

            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                var restrictions = restrictionsPool.Get(entity);

                var isOutside = position.Value.x < restrictions.Min.x || position.Value.x > restrictions.Max.x
                    || position.Value.z < restrictions.Min.z || position.Value.z > restrictions.Max.z;

                position.Value = new Vector3(
                    Mathf.Clamp(position.Value.x, restrictions.Min.x, restrictions.Max.x),
                    Mathf.Clamp(position.Value.y, restrictions.Min.y, restrictions.Max.y),
                    Mathf.Clamp(position.Value.z, restrictions.Min.z, restrictions.Max.z));

                if (isOutside && turnAnglePool.Has(entity) && !turnRequestPool.Has(entity))
                    turnRequestPool.Add(entity);
            }
        }
    }
}