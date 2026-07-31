using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class EnemyFactory
    {
        private readonly IObjectResolver _container;
        private readonly Enemy _enemyPrefab;

        [Inject]
        public EnemyFactory(IObjectResolver container, Enemy bulletPrefab)
        {
            _container = container;
            _enemyPrefab = bulletPrefab;
        }

        public Enemy Create(Transform parentTransform)
        {
            return _container.Instantiate(_enemyPrefab, parentTransform);
        }
    }
}