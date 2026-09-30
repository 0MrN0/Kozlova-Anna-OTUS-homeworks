using Code.Components;
using Code.Configs;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Views
{
    public sealed class CameraInstaller : EntityInstaller
    {
        [SerializeField] private CameraConfig _config;

        protected override void Install(Entity entity)
        {
            entity.AddData(new TransformView { Value = transform });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = Vector3.zero });
            entity.AddData(new MoveSpeed { Value = _config.MoveSpeed });
            entity.AddData(new MoveOffset { Value = Vector3.zero });
            entity.AddData(new CameraLook
            {
                Yaw = transform.eulerAngles.y,
                Pitch = Mathf.DeltaAngle(0f, transform.eulerAngles.x)
            });
            entity.AddData(new CameraSettings
            {
                LookSensitivity = _config.LookSensitivity,
                MaxPitch = _config.MaxPitch,
                MinPitch = _config.MinPitch,
                ZoomSensitivity = _config.ZoomSensitivity
            });
            entity.AddData(new PositionRestrictions
            {
                Min = _config.MinPosition,
                Max = _config.MaxPosition,
            });
            entity.AddData(new CameraInput());
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}