using Code.Components;
using Code.Services.Inputs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class CameraInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraLook, CameraSettings, Rotation, MoveDirection, Position>> _filter;
        private readonly EcsCustomInject<IInputManager> _inputManager;

        public void Run(EcsSystems systems)
        {
            var input = _inputManager.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref var look = ref _filter.Pools.Inc1.Get(entity);
                var settings = _filter.Pools.Inc2.Get(entity);
                ref var rotation = ref _filter.Pools.Inc3.Get(entity);
                ref var direction = ref _filter.Pools.Inc4.Get(entity);
                ref var position = ref _filter.Pools.Inc5.Get(entity);

                if (input.CameraRotateHeld)
                {
                    var delta = input.LookDelta * settings.LookSensitivity;
                    look.Yaw += delta.x;
                    look.Pitch = Mathf.Clamp(look.Pitch - delta.y, settings.MinPitch, settings.MaxPitch);
                }
                rotation.Value = Quaternion.Euler(look.Pitch, look.Yaw, 0f);

                var zoom = input.Zoom * settings.ZoomSensitivity;
                position.Value += rotation.Value * Vector3.forward * zoom;

                var yRotation = Quaternion.Euler(0f, look.Yaw, 0f);
                var move = input.CameraMove;
                direction.Value = yRotation * new Vector3(move.x, 0f, move.y);
            }
        }
    }
}