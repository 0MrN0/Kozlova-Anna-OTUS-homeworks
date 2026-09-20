using Zenject;

namespace Code.Gameplay.LevelField
{
    public sealed class GameFieldInstaller : Installer<GameFieldInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameField>().FromComponentInHierarchy().AsSingle();
        }
    }
}
