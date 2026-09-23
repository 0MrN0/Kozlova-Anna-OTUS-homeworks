using Zenject;

namespace Code.Gameplay.ScoreSystem
{
    public sealed class ScoreSystemInstaller : Installer<ScoreSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScoreStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().NonLazy();
        }
    }
}