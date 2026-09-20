using Code.Gameplay.Player.View;
using Zenject;

namespace Code.Gameplay.Player
{
    public sealed class PlayerInstaller : Installer<PlayerConfig, PlayerFacade, PlayerInstaller>
    {
        private readonly PlayerConfig _config;
        private readonly PlayerFacade _prefab;

        public PlayerInstaller(PlayerConfig config, PlayerFacade prefab)
        {
            _config = config;
            _prefab = prefab;
        }

        public override void InstallBindings()
        {
            Container.Bind<PlayerSpawnPoint>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_config).AsSingle();

            Container.Bind<PlayerFacade>().FromComponentInNewPrefab(_prefab).AsSingle();
            // ниже берутся инстансы с уже созданного префаба
            Container.Bind<IPlayerMoverView>().FromResolveGetter<PlayerFacade>(f => f.MoverView).AsSingle();
            Container.Bind<IPlayerAnimatorView>().FromResolveGetter<PlayerFacade>(f => f.AnimatorView).AsSingle();

            Container.BindInterfacesTo<PlayerMover>().AsSingle();
            Container.BindInterfacesTo<PlayerAnimator>().AsSingle().NonLazy();
        }
    }
}