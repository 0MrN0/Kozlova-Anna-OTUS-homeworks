using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void Run(EcsSystems systems)
        {
            var rotationPool = _rotationPool.Value;

            foreach (var entity in _filter.Value)
            {
                var transform = _filter.Pools.Inc1.Get(entity);
                var position = _filter.Pools.Inc2.Get(entity);
                
                transform.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    var rotation = rotationPool.Get(entity).Value;
                    transform.Value.rotation = rotation;
                }
            }
        }
    }
}