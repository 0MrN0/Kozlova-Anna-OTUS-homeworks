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
                .Add(new PointerWorldPositionSystem())  // точка на земле под курсором + флаг попадания в землю
                .Add(new SpawnInputSystem())            // реакция на инпут: создание запроса на спавн
                .Add(new CameraInputSystem())           // запись сырых инпут-значений камеры
                .Add(new CameraControlSystem())         // изменение look, rotation + direction, offset у камеры
                .Add(new MovementSystem())              // плавное изменение position по direction
                .Add(new MoveOffsetSystem())            // резкий разовый скачок position по offset
                .Add(new PositionRestrictionSystem())   // ограничение position + запрос на поворот
                .Add(new TurnRequestSystem())           // разовое изменение direction по запросу на поворот
                .Add(new FaceMoveDirectionSystem())     // изменение rotation по направлению direction
                .Add(new SpawnRequestSystem())          // разовый спавн по запросу на спавн
                .Add(new TransformViewSystem())         // применение position и rotation к MonoBeh.transform
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
