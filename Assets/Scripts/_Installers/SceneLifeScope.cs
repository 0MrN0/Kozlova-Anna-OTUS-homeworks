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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(levelBounds);
            builder.RegisterComponent(bulletPrefab);
            builder.Register<BulletFactory>(Lifetime.Singleton);
            builder.RegisterInstance(bulletPoolCapacity).Keyed(BulletSystemParams.bulletPoolCapacity);
            builder.RegisterInstance(bulletPoolTransform).Keyed(BulletSystemParams.bulletPoolTransform);
            builder.RegisterInstance(worldTransformForBullets).Keyed(BulletSystemParams.worldTransformForBullets);
            builder.RegisterEntryPoint<BulletSystem>(Lifetime.Singleton).AsSelf();

            builder.RegisterComponent(enemyPrefab);
            builder.Register<EnemyFactory>(Lifetime.Singleton);
        }
    }
}