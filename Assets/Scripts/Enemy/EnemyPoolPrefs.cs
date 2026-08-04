using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPoolPrefs
    {
        public Transform CharacterTransform { get; private set; }
        public Transform WorldTransform { get; private set; }
        public Transform PoolTransform { get; private set; }
        public int MaxEnemyOnScreen { get; private set; }

        public EnemyPoolPrefs(Transform characterTransform,
                              Transform worldTransform,
                              Transform poolTransform,
                              int maxEnemyOnScreen)
        {
            CharacterTransform = characterTransform;
            WorldTransform = worldTransform;
            PoolTransform = poolTransform;
            MaxEnemyOnScreen = maxEnemyOnScreen;
        }
    }
}