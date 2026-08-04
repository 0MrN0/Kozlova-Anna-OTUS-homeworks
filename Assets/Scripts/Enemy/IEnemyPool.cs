namespace ShootEmUp
{
    public interface IEnemyPool
    {
        public abstract EnemyBase SpawnEnemy();
        public abstract void UnspawnEnemy(EnemyBase enemy);
    }
}