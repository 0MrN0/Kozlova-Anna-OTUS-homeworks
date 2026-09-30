using Code.Configs;
using Code.Services.Inputs;
using Code.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private CubeConfig _cubeConfig;
        [SerializeField] private Camera _camera;

        private EcsWorld _world;
        private EcsWorld _events;
        private EcsSystems _systems;
        private EntityManager _entityManager;
        private IInputManager _inputManager;

        private void Awake()
        {
            _entityManager = new EntityManager();
            _inputManager = new InputManager();
            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);

            _systems
                .Add(new PointerWorldPositionSystem())
                .Add(new SpawnInputSystem())
                .Add(new CameraInputSystem())
                .Add(new MovementSystem())
                .Add(new PositionRestrictionSystem())
                .Add(new SpawnRequestSystem())
                .Add(new TransformViewSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS))
#endif
                ;
        }

        private void Start()
        {
            _entityManager.Initialize(_world);
            _inputManager.Enable();

            _systems.Inject(_entityManager, _inputManager, _cubeConfig, _camera);
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;

            _events?.Destroy();
            _events = null;

            _inputManager?.Dispose();
        }
    }
}
