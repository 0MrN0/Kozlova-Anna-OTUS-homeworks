using Code.Gameplay.Flower;
using Code.Gameplay.LevelField;
using Code.Gameplay.Player;
using Code.Gameplay.Player.View;
using Code.Gameplay.ScoreSystem;
using Code.Infrastructure.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public sealed class BattleSceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerFacade _playerPrefab;
        [SerializeField] private FlowerConfig[] _flowerConfigs;
        [SerializeField] private int _flowersCount = 10;

        public override void InstallBindings()
        {
            GameFieldInstaller.Install(Container);
            PlayerInstaller.Install(Container, _config, _playerPrefab);
            ScoreSystemInstaller.Install(Container);
            FlowerInstaller.Install(Container, _flowerConfigs, _flowersCount);
            Container.BindInterfacesAndSelfTo<ProgressApplier>().AsSingle().NonLazy();
        }
    }
}