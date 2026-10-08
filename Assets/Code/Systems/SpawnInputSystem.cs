using Code.Components;
using Code.Configs;
using Code.Data;
using Code.Services.Inputs;
using Code.Services.StaticTools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Systems
{
    public sealed class SpawnInputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;
        private readonly EcsFilterInject<Inc<PointerWorldPosition>> _pointerFilter;
        private readonly EcsFilterInject<Inc<CameraLook>> _cameraLookFilter;

        private readonly EcsCustomInject<IInputManager> _inputManager;
        private readonly EcsCustomInject<SpawnConfig> _config;

        public void Run(EcsSystems systems)
        {
            var input = _inputManager.Value;
            var config = _config.Value;

            var isRedRequested = input.SpawnRedCubePressed;
            var isBlueRequested = input.SpawnBlueCubePressed;

            if (!isRedRequested && !isBlueRequested)
                return;

            var rotation = GetSpawnRotation();

            foreach (var entity in _pointerFilter.Value)
            {
                var pointer = _pointerFilter.Pools.Inc1.Get(entity);
                if (!pointer.IsValid) continue;

                var spawnPoint = pointer.Value + Vector3.up * config.SpawnHeight;
                var request = _eventWorld.Value.SendSpawnRequest(config.CubePrefab, spawnPoint, rotation);

                if (isRedRequested)
                    _eventWorld.Value.SetTeam(request, TeamType.Red);
                else if (isBlueRequested)
                    _eventWorld.Value.SetTeam(request, TeamType.Blue);
            }
        }

        private Quaternion GetSpawnRotation()
        {
            foreach (var entity in _cameraLookFilter.Value)
            {
                var look = _cameraLookFilter.Pools.Inc1.Get(entity);
                return Quaternion.Euler(0f, look.Yaw, 0f);
            }

            return Quaternion.identity;
        }
    }
}
