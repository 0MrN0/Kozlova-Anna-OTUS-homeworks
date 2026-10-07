using Code.Components;
using Code.Configs;
using Code.Services.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Views
{
    public sealed class CubeInstaller : EntityInstaller
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TriggerListener _triggerListener;
        [SerializeField] private CubeConfig _config;
        [SerializeField] private FieldConfig _fieldConfig;
        [SerializeField] private SphereCollider _visionCollider;

        protected override void Install(Entity entity)
        {
            entity.AddData(new TransformView { Value = transform });
            entity.AddData(new RendererView { Value = _renderer });
            entity.AddData(new TriggerListenerView { Value = _triggerListener });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = transform.forward });
            entity.AddData(new MoveSpeed { Value = _config.Speed });
            entity.AddData(new TurnAngle { Value = _config.TurnAngle });
            entity.AddData(new PositionRestrictions
            {
                Min = _fieldConfig.MinPosition,
                Max = _fieldConfig.MaxPosition,
            });
            entity.AddData(new FaceMoveDirection());
            entity.AddData(new VisionRadius { Value = _visionCollider.radius * _visionCollider.transform.lossyScale.x });
            entity.AddData(new AttackCooldown { Duration = _config.AttackCooldown, Timer = _config.AttackCooldown });
            entity.AddData(new Health { Value = _config.Health });
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}