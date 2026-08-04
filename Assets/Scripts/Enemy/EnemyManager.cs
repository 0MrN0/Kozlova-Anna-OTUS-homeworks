using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class EnemyManager : IStartable, IDisposable
    {
        private readonly IEnemyPool _enemyPool;

        private readonly HashSet<EnemyBase> _activeEnemies = new();
        private readonly int _enemySpawnCoolDownMsec = 1000;
        private readonly CancellationTokenSource _cancelTokenSrc = new();

        [Inject]
        public EnemyManager(IEnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public void Start()
        {
            SpawnEnemiesAsync(_cancelTokenSrc.Token).Forget();
        }

        private async UniTaskVoid SpawnEnemiesAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    await UniTask.Delay(
                        _enemySpawnCoolDownMsec,
                        cancellationToken: token);

                    var enemy = _enemyPool.SpawnEnemy();

                    if (enemy != null && _activeEnemies.Add(enemy))
                    {
                        enemy.DeadEvent += OnEnemyDestroyed;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void OnEnemyDestroyed(EnemyBase enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.DeadEvent -= OnEnemyDestroyed;
                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        public void Dispose()
        {
            _cancelTokenSrc.Cancel();
            _cancelTokenSrc.Dispose();
            foreach (var e in _activeEnemies)
            {
                e.DeadEvent -= OnEnemyDestroyed;
            }
            _activeEnemies.Clear();
        }
    }
}