using Code.Components;
using Code.Services.Inputs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class CameraInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraInput>> _filter;
        private readonly EcsCustomInject<IInputManager> _inputManager;

        public void Run(EcsSystems systems)
        {
            var input = _inputManager.Value;
            var cameraInputPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                ref var cameraInput = ref cameraInputPool.Get(entity);

                cameraInput.Look = input.CameraRotateHeld ? input.LookDelta : Vector2.zero;
                cameraInput.Move = input.CameraMove;
                cameraInput.Zoom = input.Zoom;
            }
        }
    }
}