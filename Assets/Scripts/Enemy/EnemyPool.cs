using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{

    public sealed class EnemyPool : IEnemyPool, IStartable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyPoolPrefs _prefs;
        private readonly Queue<IEnemy> _enemyPool = new();

        [Inject]
        public EnemyPool(EnemyFactory enemyFactory,
                         EnemyPoolPrefs enemyPoolPrefs)
        {
            _enemyFactory = enemyFactory;
            _prefs = enemyPoolPrefs;
        }

        public void Start()
        {
            for (var i = 0; i < _prefs.MaxEnemyOnScreen; i++)
            {
                var enemy = _enemyFactory.Create(_prefs.PoolTransform);
                _enemyPool.Enqueue(enemy);
            }
        }

        public IEnemy SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.ComponentsHolder.transform.SetParent(_prefs.WorldTransform);
            enemy.Subscribe();

            var spawnPosition = _prefs.EnemyPositions.RandomSpawnPosition();
            enemy.ComponentsHolder.transform.position = spawnPosition.position;

            var attackPosition = _prefs.EnemyPositions.RandomAttackPosition();
            enemy.SetDestination(attackPosition.position);

            enemy.SetTarget(_prefs.CharacterTransform);
            return enemy;
        }

        public void UnspawnEnemy(IEnemy enemy)
        {
            enemy.Unsubscribe();
            enemy.ComponentsHolder.transform.SetParent(_prefs.PoolTransform);
            _enemyPool.Enqueue(enemy);
        }
    }
}