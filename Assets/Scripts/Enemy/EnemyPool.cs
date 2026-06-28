using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private BulletSystem bulletSystem;

        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private Enemy prefab;

        private readonly Queue<Enemy> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(prefab, container);
                enemyPool.Enqueue(enemy);
            }
        }

        public Enemy SpawnEnemy()
        {
            if (!enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }

            enemy.transform.SetParent(worldTransform);
            enemy.Init(bulletSystem);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);

            enemy.AttackAgent.SetTarget(characterTransform);
            return enemy;
        }

        public void UnspawnEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(container);
            enemyPool.Enqueue(enemy);
        }
    }
}