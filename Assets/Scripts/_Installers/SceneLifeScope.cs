using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public enum BulletSystemParams
    {
        bulletPoolCapacity,
        bulletPoolTransform,
        worldTransformForBullets,
    }

    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [Header("BulletSystem")]
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int bulletPoolCapacity = 50;
        [SerializeField] private Transform bulletPoolTransform;
        [SerializeField] private Transform worldTransformForBullets;

        [Header("EnemySystem")]
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform[] enemySpawnPositions;
        [SerializeField] private Transform[] enemyAttackPositions;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Transform poolTransform;
        [SerializeField] private int maxEnemyOnScreen = 7;

        [Header("PlayerSystem")]
        [SerializeField] private CharacterComponentsHolder characterComponentsHolder;
        [SerializeField] private PlayerBulletConfig playerBulletConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(levelBounds);
            builder.RegisterComponent(bulletPrefab);
            builder.Register<BulletFactory>(Lifetime.Singleton);
            builder.RegisterInstance(bulletPoolCapacity).Keyed(BulletSystemParams.bulletPoolCapacity);
            builder.RegisterInstance(bulletPoolTransform).Keyed(BulletSystemParams.bulletPoolTransform);
            builder.RegisterInstance(worldTransformForBullets).Keyed(BulletSystemParams.worldTransformForBullets);
            builder.RegisterEntryPoint<BulletSystem>(Lifetime.Singleton).AsSelf(); // RegisterEntryPoint = Register.AsImplementedInterfaces + регистрация в цикле

            builder.RegisterComponent(enemyPrefab);
            builder.Register<EnemyFactory>(Lifetime.Singleton);
            
            var enemyPositions = new EnemyPositions(enemySpawnPositions, enemyAttackPositions);
            var enemyPoolPrefs = new EnemyPoolPrefs(enemyPositions, characterTransform, worldTransform, poolTransform, maxEnemyOnScreen);
            builder.RegisterInstance(enemyPoolPrefs);
            builder.RegisterEntryPoint<EnemyPool>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<EnemyManager>(Lifetime.Singleton).AsSelf();

            builder.RegisterComponent(characterComponentsHolder);
            builder.RegisterEntryPoint<InputManager>(Lifetime.Singleton).AsSelf();
            builder.Register<GameManager>(Lifetime.Singleton);
            builder.RegisterInstance(playerBulletConfig);

            builder.RegisterEntryPoint<CharacterAttackAgent>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<CharacterDeathAgent>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<CharacterMoveAgent>(Lifetime.Singleton).AsSelf();
        }
    }
}