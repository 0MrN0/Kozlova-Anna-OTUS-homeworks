using Code.Components;
using Code.Configs;
using Code.Data;
using Code.Services.Inputs;
using Code.Services.Physics;
using Code.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Code
{
    public sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private TeamConfig _teamConfig;
        [SerializeField] private CubeConfig _cubeConfig;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Camera _camera;

        private EcsWorld _world;
        private EcsWorld _events;
        private EcsSystems _systems;
        private EntityManager _entityManager;
        private IInputManager _inputManager;
        private ITriggerEventSink _triggerEventSink;

        private void Awake()
        {
            _entityManager = new EntityManager();
            _inputManager = new InputManager();
            _world = new EcsWorld();
            _events = new EcsWorld();
            _triggerEventSink = new EcsTriggerEventSink(_world, _events);
            _systems = new EcsSystems(_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);

            _systems
                .Add(new PointerWorldPositionSystem())  // точка на земле под курсором + флаг попадания в землю
                .Add(new SpawnInputSystem())            // реакция на инпут: создание запроса на спавн
                .Add(new CameraInputSystem())           // запись сырых инпут-значений камеры
                .Add(new CameraControlSystem())         // изменение look, rotation + direction, offset у камеры
                .Add(new VisionSystem())                // обработка попадания в зону видимости
                .DelHere<TriggerEnterEvent>(EcsWorlds.EVENTS)
                .Add(new TargetValidationSystem())       // снять плохую цель → SearchTarget
                .Add(new TargetSearchSystem())           // OverlapSphere → новая цель
                .Add(new MovementLockSystem())           // MovementLock = цель || Dead
                .Add(new FaceTargetSystem())
                .Add(new AttackSystem())
                .Add(new MovementSystem())              // плавное изменение position по direction
                .Add(new MoveOffsetSystem())            // резкий разовый скачок position по offset
                .Add(new PositionRestrictionSystem())   // ограничение position + запрос на поворот
                .Add(new TurnRequestSystem())           // разовое изменение direction по запросу на поворот
                .Add(new FaceMoveDirectionSystem())     // изменение rotation по направлению direction
                .Add(new LifetimeSystem())
                .Add(new SpawnRequestSystem())          // разовый спавн по запросу на спавн
                .Add(new TriggerListenerInitSystem())   // разово проинициализировать заспавненный TriggerListener
                .Add(new ApplyTeamViewSystem())         // применить команду всем, кому еще не применено
                .Add(new TransformViewSystem())         // применение position и rotation к MonoBeh.transform
                .Add(new DestroySystem())
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

            _systems.Inject(_entityManager, _inputManager,
                                _triggerEventSink,
                                _teamConfig, _cubeConfig, _bulletConfig,
                                _camera);
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
