using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class EnemyPoolPrefs
    {
        public EnemyPositions EnemyPositions { get; private set; }
        public Transform CharacterTransform { get; private set; }
        public Transform WorldTransform { get; private set; }
        public Transform PoolTransform { get; private set; }
        public int MaxEnemyOnScreen { get; private set; }

        public EnemyPoolPrefs(EnemyPositions enemyPositions,
                                      Transform characterTransform,
                                      Transform worldTransform,
                                      Transform poolTransform,
                                      int maxEnemyOnScreen)
        {
            EnemyPositions = enemyPositions;
            CharacterTransform = characterTransform;
            WorldTransform = worldTransform;
            PoolTransform = poolTransform;
            MaxEnemyOnScreen = maxEnemyOnScreen;
        }
    }
}