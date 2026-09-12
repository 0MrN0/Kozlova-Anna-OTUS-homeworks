using Code.Infrastructure.DI.ModeDI;
using Zenject;

namespace Code.Infrastructure.DI
{
    public sealed class DiInstaller : Installer<DiInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IProjectDiService>().To<ProjectDiService>().AsSingle();
            Container.Bind<IModeDiService>().To<ModeDiService>().AsSingle();
        }
    }
}