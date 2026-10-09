using Code.Configs;
using Code.Data;
using Code.Services.StaticTools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class StartSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<SpawnConfig> _spawnConfig;
        private readonly EcsCustomInject<FieldConfig> _fieldConfig;

        public void Init(EcsSystems systems)
        {
            var field = _fieldConfig.Value;
            var config = _spawnConfig.Value;

            var redRowZ = field.MinPosition.z + config.StartEdgeOffset;
            var blueRowZ = field.MaxPosition.z - config.StartEdgeOffset;

            SpawnFormation(TeamType.Red, redRowZ, Vector3.forward);
            SpawnFormation(TeamType.Blue, blueRowZ, Vector3.back);
        }

        private void SpawnFormation(TeamType team, float frontRowZ, Vector3 direction)
        {
            var field = _fieldConfig.Value;
            var config = _spawnConfig.Value;

            var rotation = Quaternion.LookRotation(direction);
            var columns = Mathf.Max(1, config.StartColumns);
            var halfWidth = (columns - 1) * config.StartSpacing * 0.5f;

            for (var i = 0; i < config.StartCountPerTeam; i++)
            {
                var column = i % columns;
                var row = i / columns;

                var x = Mathf.Clamp(column * config.StartSpacing - halfWidth, field.MinPosition.x, field.MaxPosition.x);
                var z = frontRowZ - direction.z * row * config.StartSpacing;
                var position = new Vector3(x, config.SpawnHeight, z);

                var request = _eventWorld.Value.SendSpawnRequest(config.CubePrefab, position, rotation);
                _eventWorld.Value.SetTeam(request, team);
            }
        }
    }
}
