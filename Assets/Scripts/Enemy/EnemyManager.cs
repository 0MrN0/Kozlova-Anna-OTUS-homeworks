using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPoolBase enemyPool;

        private readonly HashSet<EnemyBase> _activeEnemies = new();
        private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1);

        private IEnumerator Start()
        {
            while (true)
            {
                yield return _waitForSeconds1;
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

        private void OnDestroyed(EnemyBase enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.DeadEvent -= OnDestroyed;
                enemyPool.UnspawnEnemy(enemy);
            }
        }
    }
}