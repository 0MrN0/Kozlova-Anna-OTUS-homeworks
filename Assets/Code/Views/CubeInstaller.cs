using Code.Components;
using Code.Configs;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Views
{
    public sealed class CubeInstaller : EntityInstaller
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private CubeConfig _cubeConfig;
        [SerializeField] private FieldConfig _fieldConfig;

        protected override void Install(Entity entity)
        {
            entity.AddData(new TransformView { Value = transform });
            entity.AddData(new RendererView { Value = _renderer });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = transform.forward });
            entity.AddData(new MoveSpeed { Value = _cubeConfig.Speed });
            entity.AddData(new TurnAngle { Value = _cubeConfig.TurnAngle });
            entity.AddData(new PositionRestrictions
            {
                Min = _fieldConfig.MinPosition,
                Max = _fieldConfig.MaxPosition,
            });
            entity.AddData(new FaceMoveDirection());
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}