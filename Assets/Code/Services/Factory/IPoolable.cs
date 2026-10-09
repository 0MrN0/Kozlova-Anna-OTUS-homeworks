namespace Code.Services.Factory
{
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }
}