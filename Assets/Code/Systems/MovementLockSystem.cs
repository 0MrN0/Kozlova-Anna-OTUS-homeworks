using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class MovementLockSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection>> _filter;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsPoolInject<AttackTarget> _targetPool;
        private readonly EcsPoolInject<MovementLock> _lockPool;

        public void Run(EcsSystems systems)
        {
            var deadPool = _deadPool.Value;
            var targetPool = _targetPool.Value;
            var lockPool = _lockPool.Value;

            foreach (var entity in _filter.Value)
            {
                if (!lockPool.Has(entity) 
                        && (deadPool.Has(entity) || targetPool.Has(entity)))
                {
                    lockPool.Add(entity);
                }
                else if (lockPool.Has(entity)
                            && !deadPool.Has(entity)
                            && !targetPool.Has(entity))
                {
                    lockPool.Del(entity);
                }
            }
        }
    }
}