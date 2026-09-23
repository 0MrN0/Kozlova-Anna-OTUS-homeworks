using Zenject;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class SaveLoadInstaller : Installer<SaveLoadInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<ISaveStorage>().To<FileSaveStorage>().AsSingle();
            Container.Bind<ISaveLoadAggregate>().To<SaveLoadAggregate>().AsSingle();
        }
    }
}
