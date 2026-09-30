using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class CameraControlSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CameraInput, CameraSettings, CameraLook, Rotation, MoveDirection, MoveOffset>> _filter;

        public void Run(EcsSystems systems)
        {
            var inputPool = _filter.Pools.Inc1;
            var settingsPool = _filter.Pools.Inc2;
            var lookPool = _filter.Pools.Inc3;
            var rotationPool = _filter.Pools.Inc4;
            var directionPool = _filter.Pools.Inc5;
            var offsetPool = _filter.Pools.Inc6;

            foreach (var entity in _filter.Value)
            {
                var input = inputPool.Get(entity);
                var settings = settingsPool.Get(entity);
                ref var look = ref lookPool.Get(entity);
                ref var rotation = ref rotationPool.Get(entity);
                ref var direction = ref directionPool.Get(entity);
                ref var offset = ref offsetPool.Get(entity);

                var delta = input.Look * settings.LookSensitivity;
                look.Yaw += delta.x;
                look.Pitch = Mathf.Clamp(look.Pitch - delta.y, settings.MinPitch, settings.MaxPitch);

                rotation.Value = Quaternion.Euler(look.Pitch, look.Yaw, 0f);

                var yawRotation = Quaternion.Euler(0f, look.Yaw, 0f);
                direction.Value = yawRotation * new Vector3(input.Move.x, 0f, input.Move.y);

                offset.Value = rotation.Value * Vector3.forward * (input.Zoom * settings.ZoomSensitivity);
            }
        }
    }
}