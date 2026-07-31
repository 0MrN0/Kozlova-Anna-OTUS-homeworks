using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class EnemyPool : EnemyPoolBase
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform poolTransform;
        [SerializeField] private Enemy prefab;
        [SerializeField] private int maxEnemyOnScreen = 7;

        private EnemyFactory _enemyFactory;

        private readonly Queue<EnemyBase> _enemyPool = new();

        [Preserve]
        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            for (var i = 0; i < maxEnemyOnScreen; i++)
            {
                var enemy = _enemyFactory.Create(poolTransform);
                _enemyPool.Enqueue(enemy);
            }
        }

        public override EnemyBase SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);
            enemy.Init();
            enemy.Subscribe();

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);

            enemy.AttackAgent.SetTarget(characterTransform);
            return enemy;
        }

        public override void UnspawnEnemy(EnemyBase enemy)
        {
            enemy.Unsubscribe();
            enemy.transform.SetParent(poolTransform);
            _enemyPool.Enqueue(enemy);
        }
    }
}