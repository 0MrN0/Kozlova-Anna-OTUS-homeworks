using Code.Services.Physics;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Services.Views
{
    public sealed class TriggerListener : MonoBehaviour
    {
        [SerializeField] private Entity _owner;

        private ITriggerEventSink _sink;

        public void Init(ITriggerEventSink sink)
        {
            _sink = sink;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_sink == null || !_owner.IsAlive())
            {
                return;
            }

            var otherEntity = other.GetComponentInParent<Entity>();
            if (otherEntity == null || !otherEntity.IsAlive())
            {
                return;
            }

            _sink.TriggerEntered(_owner, otherEntity);
        }
    }
}