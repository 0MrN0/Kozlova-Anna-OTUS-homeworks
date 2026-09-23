using UnityEngine;
using Code.Infrastructure.DI;
using Code.Infrastructure.SceneLoad;
using Zenject;
using Code.GameModes;
using Code.Infrastructure.Inputs;
using Code.Infrastructure.SaveLoad;

namespace Code.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        public override void InstallBindings()
        {
            DiInstaller.Install(Container);
            SceneLoadingInstaller.Install(Container, _curtainPrefab);
            InputInstaller.Install(Container);
            GameModeInstaller.Install(Container);
            SaveLoadInstaller.Install(Container);
        }
    }
}