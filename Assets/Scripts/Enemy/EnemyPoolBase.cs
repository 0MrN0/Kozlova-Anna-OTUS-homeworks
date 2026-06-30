using UnityEngine;

namespace ShootEmUp
{
    public abstract class EnemyPoolBase : MonoBehaviour
    {
        public abstract EnemyBase SpawnEnemy();
        public abstract void UnspawnEnemy(EnemyBase enemy);
    }
}