using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : EnemyPoolBase
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private BulletSystem bulletSystem;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private Enemy prefab;
        [SerializeField] private int maxEnemyOnScreen = 7;

        private readonly Queue<EnemyBase> _enemyPool = new();

        private void Awake()
        {
            for (var i = 0; i < maxEnemyOnScreen; i++)
            {
                var enemy = Instantiate(prefab, container);
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
            enemy.Init(bulletSystem);
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
            enemy.transform.SetParent(container);
            _enemyPool.Enqueue(enemy);
        }
    }
}