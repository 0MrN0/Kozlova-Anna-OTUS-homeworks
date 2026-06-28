using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool enemyPool;
        
        private readonly HashSet<Enemy> _activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (_activeEnemies.Add(enemy))
                    {
                        enemy.DeadEvent += OnDestroyed;
                    }
                }
            }
        }

        private void OnDestroyed(Enemy enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.DeadEvent -= OnDestroyed;
                enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}