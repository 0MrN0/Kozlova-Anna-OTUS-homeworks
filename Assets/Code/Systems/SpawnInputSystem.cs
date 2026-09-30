using Code.Components;
using Code.Configs;
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

        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
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

            foreach (var entity in _pointerFilter.Value)
            {
                var pointer = _pointerFilter.Pools.Inc1.Get(entity);

                if (!pointer.IsValid)
                    continue;

                var spawnPoint = pointer.Value + Vector3.up * config.SpawnHeight;

                if (isRedRequested)
                    CreateSpawnRequest(spawnPoint, config.RedCubePrefab);

                if (isBlueRequested)
                    CreateSpawnRequest(spawnPoint, config.BlueCubePrefab);
            }
        }

        private void CreateSpawnRequest(Vector3 worldPoint, Entity prefab)
        {
            var entity = _eventWorld.Value.NewEntity();
            _spawnRequestPool.Value.Add(entity);
            _positionPool.Value.Add(entity).Value = worldPoint;
            _rotationPool.Value.Add(entity).Value = Quaternion.identity;
            _prefabPool.Value.Add(entity).Value = prefab;
        }
    }
}
