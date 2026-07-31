using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class BulletFactory
    {
        private readonly IObjectResolver _container;
        private readonly Bullet _bulletPrefab;

        [Inject]
        public BulletFactory(IObjectResolver container, Bullet bulletPrefab)
        {
            _container = container;
            _bulletPrefab = bulletPrefab;
        }

        public Bullet Create(Transform parentTransform)
        {
            return _container.Instantiate(_bulletPrefab, parentTransform);
        }
    }
}