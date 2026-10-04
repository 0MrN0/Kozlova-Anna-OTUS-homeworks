using Leopotam.EcsLite.Entities;

namespace Code.Services.Physics
{
    public interface ITriggerEventSink
    {
        void TriggerEntered(Entity owner, Entity other);
    }
}