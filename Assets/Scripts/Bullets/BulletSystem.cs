using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class BulletSystem: IStartable, IFixedTickable
    {
        private readonly BulletFactory _bulletFactory;
        private readonly LevelBounds _levelBounds;
        private readonly int _initialCount;
        private readonly Transform _poolTransform;
        private readonly Transform _worldTransform;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        [Inject]
        public BulletSystem(BulletFactory bulletFactory, 
                            LevelBounds levelBounds, 
                            [Key(BulletSystemParams.bulletPoolCapacity)] int initialCount, 
                            [Key(BulletSystemParams.bulletPoolTransform)] Transform poolTransform, 
                            [Key(BulletSystemParams.worldTransformForBullets)] Transform worldTransform)
        {
            _bulletFactory = bulletFactory;
            _levelBounds = levelBounds;
            _initialCount = initialCount;
            _poolTransform = poolTransform;
            _worldTransform = worldTransform;
        }

        public void Start()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = _bulletFactory.Create(_poolTransform);
                _bulletPool.Enqueue(bullet);
            }
        }

        public void FixedTick()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (var i = 0; i < _cache.Count; i++)
            {
                var bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletArgs args)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = _bulletFactory.Create(_worldTransform);
            }

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.Damage = args.damage;
            bullet.IsPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_poolTransform);
                _bulletPool.Enqueue(bullet);
            }
        }

    }
}