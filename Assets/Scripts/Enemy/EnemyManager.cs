using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, ISceneCycleStart 
    {
        [SerializeField] private EnemyPoolBase enemyPool;

        private readonly HashSet<EnemyBase> _activeEnemies = new();
        private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1);

        public void OnStart()
        {
            StartCoroutine(EnemySpawnRoutine());
        }

        private IEnumerator EnemySpawnRoutine()
        {
            while (true)
            {
                yield return _waitForSeconds1;
                var enemy = enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (_activeEnemies.Add(enemy))
                    {
                        enemy.DeadEvent += HandleEnemyDestroyed;
                    }
                }
            }
        }

        private void HandleEnemyDestroyed(EnemyBase enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.DeadEvent -= HandleEnemyDestroyed;
                enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}