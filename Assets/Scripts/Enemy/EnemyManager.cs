using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour, ISceneCycleStart, ISceneCyclePause, ISceneCycleResume
    {
        [SerializeField] private EnemyPoolBase enemyPool;

        private readonly HashSet<EnemyBase> _activeEnemies = new();
        private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1);
        private bool _isPaused = false;

        public void OnStart()
        {
            StartCoroutine(EnemySpawnRoutine());
        }

        public void OnPause()
        {
            _isPaused = true;
        }

        public void OnResume()
        {
            _isPaused = false;
        }

        private IEnumerator EnemySpawnRoutine()
        {
            while (true)
            {
                yield return _waitForSeconds1;

                while (_isPaused) yield return null;

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