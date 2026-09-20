using Zenject;

namespace Code.Gameplay.Flower
{
    public sealed class FlowerInstaller : Installer<FlowerConfig[], int, FlowerInstaller>
    {
        private readonly FlowerConfig[] _configs;
        private readonly int _flowersCount;

        public FlowerInstaller(FlowerConfig[] configs, int flowersCount)
        {
            _configs = configs;
            _flowersCount = flowersCount;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FlowerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<FlowerSpawner>()
                      .AsSingle()
                      .WithArguments(_configs, _flowersCount)
                      .NonLazy();
        }
    }
}