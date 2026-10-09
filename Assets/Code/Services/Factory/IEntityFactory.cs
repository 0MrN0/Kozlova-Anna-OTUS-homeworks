using UnityEngine;
using Leopotam.EcsLite.Entities;

namespace Code.Services.Factory
{
    public interface IEntityFactory
    {
        Entity Spawn(Entity prefab, Vector3 position, Quaternion rotation);
        void Despawn(int entity);
    }
}