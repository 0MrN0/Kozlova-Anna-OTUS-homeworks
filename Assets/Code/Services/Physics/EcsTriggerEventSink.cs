using Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;

namespace Code.Services.Physics
{
    public sealed class EcsTriggerEventSink : ITriggerEventSink
    {
        private readonly EcsWorld _world;
        private readonly EcsWorld _events;

        public EcsTriggerEventSink(EcsWorld world, EcsWorld events)
        {
            _world = world;
            _events = events;
        }

        public void TriggerEntered(Entity owner, Entity other)
        {
            if (!_events.IsAlive())
            {
                return;
            }

            var entity = _events.NewEntity();
            ref var triggerEvent = ref _events.GetPool<TriggerEnterEvent>().Add(entity);
            triggerEvent.Owner = _world.PackEntity(owner.Id);
            triggerEvent.Other = _world.PackEntity(other.Id);
        }
    }
}