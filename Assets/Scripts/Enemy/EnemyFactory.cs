using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class EnemyFactory
    {
        private readonly IObjectResolver _container;
        private readonly EnemyComponentsHolder _enemyComponentsPrefab;

        [Inject]
        public EnemyFactory(IObjectResolver container, EnemyComponentsHolder enemyComponentsPrefab)
        {
            _container = container;
            _enemyComponentsPrefab = enemyComponentsPrefab;
        }

        public Enemy Create(Transform parentTransform)
        {
            var enemyComponents = _container.Instantiate(_enemyComponentsPrefab, parentTransform);
            enemyComponents.Init();
            var enemy = _container.Resolve<Enemy>();
            enemy.Init(enemyComponents);
            return enemy;
        }
    }
}