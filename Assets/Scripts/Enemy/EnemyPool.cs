using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{

    public sealed class EnemyPool : IEnemyPool, IStartable
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyPositions _enemySpawnPositions;
        private readonly EnemyPoolPrefs _prefs;
        private readonly Queue<EnemyBase> _enemyPool = new();

        [Inject]
        public EnemyPool(EnemyFactory enemyFactory,
                          EnemyPositions enemyPositions,
                          EnemyPoolPrefs enemyPoolPrefs)
        {
            _enemyFactory = enemyFactory;
            _enemySpawnPositions = enemyPositions;
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

        public EnemyBase SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_prefs.WorldTransform);
            enemy.Init();
            enemy.Subscribe();

            var spawnPosition = _enemySpawnPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemySpawnPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);

            enemy.AttackAgent.SetTarget(_prefs.CharacterTransform);
            return enemy;
        }

        public void UnspawnEnemy(EnemyBase enemy)
        {
            enemy.Unsubscribe();
            enemy.transform.SetParent(_prefs.PoolTransform);
            _enemyPool.Enqueue(enemy);
        }
    }
}