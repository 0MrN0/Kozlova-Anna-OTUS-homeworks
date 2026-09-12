using Code.GameModes.Machine;
using Zenject;

namespace Code.GameModes
{
    public class GameModeInstaller : Installer<GameModeInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameModeMachine>().AsSingle();

            Container.BindInterfacesTo<BootMode>().AsSingle();
            Container.BindInterfacesTo<MetaMode>().AsSingle();
            Container.BindInterfacesTo<BattleMode>().AsSingle();
        }
    }
}