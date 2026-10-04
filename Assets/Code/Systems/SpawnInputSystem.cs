using Code.Components;
using Code.Configs;
using Code.Data;
using Code.Services.Inputs;
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

        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Team> _teamPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<IInputManager> _inputManager;
        private readonly EcsCustomInject<CubeConfig> _cubeConfig;

        public void Run(EcsSystems systems)
        {
            var input = _inputManager.Value;
            var config = _cubeConfig.Value;

            var isRedRequested = input.SpawnRedCubePressed;
            var isBlueRequested = input.SpawnBlueCubePressed;

            if (!isRedRequested && !isBlueRequested)
                return;

            var rotation = GetSpawnRotation();

            foreach (var entity in _pointerFilter.Value)
            {
                var pointer = _pointerFilter.Pools.Inc1.Get(entity);

                if (!pointer.IsValid)
                    continue;

                var spawnPoint = pointer.Value + Vector3.up * config.SpawnHeight;

                if (isRedRequested)
                    CreateSpawnRequest(spawnPoint, rotation, config.CubePrefab, TeamType.Red);

                if (isBlueRequested)
                    CreateSpawnRequest(spawnPoint, rotation, config.CubePrefab, TeamType.Blue);
            }
        }

        private void CreateSpawnRequest(Vector3 worldPoint, Quaternion rotation, Entity prefab, TeamType teamType)
        {
            var entity = _eventWorld.Value.NewEntity();
            _spawnRequestPool.Value.Add(entity);
            _positionPool.Value.Add(entity).Value = worldPoint;
            _rotationPool.Value.Add(entity).Value = rotation;
            _teamPool.Value.Add(entity).Value = teamType;
            _prefabPool.Value.Add(entity).Value = prefab;
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
