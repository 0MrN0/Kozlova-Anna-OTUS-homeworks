using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using Code.Components;
using Code.Data;

namespace Code.Services.StaticTools
{
    public static class SpawnRequestExtensions
    {
        public static int SendSpawnRequest(this EcsWorld world, Entity prefab, Vector3 position, Quaternion rotation)
        {
            var created = world.NewEntity();
            world.GetPool<SpawnRequest>().Add(created) = new SpawnRequest
            {
                Prefab = prefab,
                Position = position,
                Rotation = rotation,
            };
            return created;
        }

        public static void SetTeam(this EcsWorld events, int request, TeamType team)
        {
            events.GetPool<Team>().Add(request).Value = team;
        }
    }
}