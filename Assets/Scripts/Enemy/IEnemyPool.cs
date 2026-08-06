namespace ShootEmUp
{
    public interface IEnemyPool
    {
        public abstract IEnemy SpawnEnemy();
        public abstract void UnspawnEnemy(IEnemy enemy);
    }
}