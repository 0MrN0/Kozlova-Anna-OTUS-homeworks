using Zenject;

namespace Code.Infrastructure.SceneLoad
{
    public sealed class SceneLoadingInstaller : Installer<LoadingCurtain, SceneLoadingInstaller>
    {
        private readonly LoadingCurtain _curtainPrefab;

        public SceneLoadingInstaller(LoadingCurtain curtainPrefab)
        {
            _curtainPrefab = curtainPrefab;
        }

        public override void InstallBindings()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ILoadingCurtain>().FromComponentInNewPrefab(_curtainPrefab).AsSingle();
        }
    }
}