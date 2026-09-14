using Zenject;

namespace Code.Infrastructure.Inputs
{
    public sealed class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }
    }
}