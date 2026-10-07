using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace Code.Systems
{
    public sealed class DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestroyRequest>> _filter;
        private readonly EcsCustomInject<EntityManager> _ecsManager;

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _ecsManager.Value.Destroy(entity);
            }
        }
    }
}