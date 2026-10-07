using Code.Components;
using Code.Configs;
using Code.Services.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Views
{
    public sealed class BulletInstaller : EntityInstaller
    {
        [SerializeField] private BulletConfig _config;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TriggerListener _triggerListener;

        protected override void Install(Entity entity)
        {
            entity.AddData(new TransformView { Value = transform });
            entity.AddData(new RendererView { Value = _renderer });
            entity.AddData(new TriggerListenerView { Value = _triggerListener });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = transform.forward });
            entity.AddData(new MoveSpeed { Value = _config.Speed });
            entity.AddData(new Damage { Value = _config.Damage });
            entity.AddData(new Lifetime { Value = _config.Lifetime });
        }

        protected override void Dispose(Entity entity)
        {

        }
    }
}