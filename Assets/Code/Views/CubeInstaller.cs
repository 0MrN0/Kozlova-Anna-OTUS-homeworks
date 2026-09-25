using Code.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Views
{
    public sealed class CubeInstaller : EntityInstaller
    {
        [SerializeField] private float _moveSpeed = 5f;

        protected override void Install(Entity entity)
        {
            entity.AddData(new TransformView { Value = transform });

            entity.AddData(new Position { Value = transform.position });
            entity.AddData(new Rotation { Value = transform.rotation });
            entity.AddData(new MoveDirection { Value = Vector3.forward});
            entity.AddData(new MoveSpeed { Value = _moveSpeed });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}