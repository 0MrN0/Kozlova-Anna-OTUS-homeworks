using Code.Components;
using Code.Configs;
using Code.Services.Inputs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class PointerWorldPositionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float MaxDistance = 200f;

        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<PointerWorldPosition>> _filter;

        private readonly EcsCustomInject<IInputManager> _inputManager;
        private readonly EcsCustomInject<Camera> _camera;
        private readonly EcsCustomInject<LayerMaskConfig> _maskConfig;

        private int _groundLayerMask;

        public void Init(EcsSystems systems)
        {
            _groundLayerMask = _maskConfig.Value.GroundMask.value;

            var entity = _world.Value.NewEntity();
            _filter.Pools.Inc1.Add(entity);
        }

        public void Run(EcsSystems systems)
        {
            var ray = _camera.Value.ScreenPointToRay(_inputManager.Value.PointerPosition);
            var isHit = Physics.Raycast(ray, out var hit, MaxDistance, _groundLayerMask);

            foreach (var entity in _filter.Value)
            {
                ref var pointer = ref _filter.Pools.Inc1.Get(entity);

                pointer.IsValid = isHit;
                pointer.Value = hit.point;
            }
        }
    }
}
