namespace Code.GameModes
{
    public interface IGameMode
    {
        public void Enter();
        public void Exit();
        public void Tick();
    }
}
