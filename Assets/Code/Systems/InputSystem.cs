using Code.Components;
using Code.Configs;
using Code.Services.Inputs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Systems
{
    public sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS;

        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS;
        private readonly EcsPoolInject<PrefabComponent> _prefabPool = EcsWorlds.EVENTS;

        private readonly EcsCustomInject<IInputManager> _inputManager;
        private readonly EcsCustomInject<CubeConfig> _cubeConfig;

        public void Run(EcsSystems systems)
        {
            var input = _inputManager.Value;
            var config = _cubeConfig.Value;

            if (input.SpawnRedCubePressed)
                CreateSpawnRequest(input.PointerPosition, config.RedCubePrefab);

            if (input.SpawnBlueCubePressed)
                CreateSpawnRequest(input.PointerPosition, config.RedCubePrefab);
        }


        private void CreateSpawnRequest(Vector2 screenPoint, Entity prefab)
        {
            var worldPoint = Vector3.zero;

            var e = _eventWorld.Value.NewEntity();
            _spawnRequestPool.Value.Add(e);
            _positionPool.Value.Add(e).Value = worldPoint;
            _rotationPool.Value.Add(e).Value = Quaternion.identity;
            _prefabPool.Value.Add(e).Value = prefab;
        }
    }
}