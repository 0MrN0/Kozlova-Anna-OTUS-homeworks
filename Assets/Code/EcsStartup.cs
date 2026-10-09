using Code.Components;
using Code.Configs;
using Code.Data;
using Code.Services.Factory;
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
        [SerializeField] private SpawnConfig _spawnConfig;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private LayerMaskConfig _maskConfig;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private PoolsConfig _poolConfig;

        private EcsWorld _world;
        private EcsWorld _events;
        private EcsSystems _systems;
        private PoolEntityFactory _entityFactory;
        private IInputManager _inputManager;
        private ITriggerEventSink _triggerEventSink;

        private void Awake()
        {
            _world = new EcsWorld();
            _events = new EcsWorld();
            _entityFactory = new PoolEntityFactory(_world, _poolContainer);
            _inputManager = new InputManager();
            _triggerEventSink = new EcsTriggerEventSink(_world, _events);
            _systems = new EcsSystems(_world);
            _systems.AddWorld(_events, EcsWorlds.EVENTS);

            _systems
                .Add(new PointerWorldPositionSystem())
                .Add(new SpawnInputSystem())
                .Add(new CameraInputSystem())
                .Add(new CameraControlSystem())
                .Add(new VisionSystem())
                .Add(new HitSystem())
                .DelHere<TriggerEnterEvent>(EcsWorlds.EVENTS)
                .Add(new DamageSystem())
                .Add(new DeathSystem())
                .Add(new TargetValidationSystem())
                .Add(new TargetSearchSystem())
                .Add(new MovementLockSystem())
                .Add(new FaceTargetSystem())
                .Add(new AttackSystem())
                .Add(new MovementSystem())
                .Add(new MoveOffsetSystem())
                .Add(new PositionRestrictionSystem())
                .Add(new TurnRequestSystem())
                .Add(new FaceMoveDirectionSystem())
                .Add(new LifetimeSystem())
                .Add(new DeathTimerSystem())
                .Add(new SpawnRequestSystem())
                .Add(new TriggerListenerInitSystem())
                .Add(new ApplyTeamViewSystem())
                .Add(new AnimatorSystem())
                .DelHere<AttackPerformed>()
                .DelHere<Died>()
                .Add(new TransformViewSystem())
                .Add(new DestroySystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.EVENTS))
#endif
                ;
        }

        private void Start()
        {
            _entityFactory.RegisterSceneEntities();
            foreach (var entry in _poolConfig.Entries)
            {
                _entityFactory.Prewarm(entry.Prefab, entry.Count);
            }

            _inputManager.Enable();

            _systems.Inject(_entityFactory, _inputManager,
                                _triggerEventSink,
                                _teamConfig, _spawnConfig, _bulletConfig, _maskConfig,
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

        [ContextMenu("Log Pool Stats")]
        private void LogPoolStats() => Debug.Log(_entityFactory.GetStats());

    }
}
