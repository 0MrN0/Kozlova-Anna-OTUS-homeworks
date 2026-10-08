using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class AnimatorSystem : IEcsRunSystem
    {
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int DeathHash = Animator.StringToHash("Death");

        private readonly EcsFilterInject<Inc<AnimatorView>> _filter;
        private readonly EcsPoolInject<MovementLock> _lockPool;
        private readonly EcsPoolInject<AttackPerformed> _attackedPool;
        private readonly EcsPoolInject<Died> _diedPool;

        public void Run(EcsSystems systems)
        {
            var animatorPool = _filter.Pools.Inc1;
            var lockPool = _lockPool.Value;
            var attackedPool = _attackedPool.Value;
            var diedPool = _diedPool.Value;

            foreach (var entity in _filter.Value)
            {
                var animator = animatorPool.Get(entity).Value;

                animator.SetBool(IsMovingHash, !lockPool.Has(entity));

                if (attackedPool.Has(entity))
                    animator.SetTrigger(AttackHash);

                if (diedPool.Has(entity))
                    animator.SetTrigger(DeathHash);
            }
        }
    }
}