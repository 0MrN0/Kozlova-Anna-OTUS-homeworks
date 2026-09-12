using UnityEngine;
using Code.Infrastructure.DI;
using Code.Infrastructure.SceneLoad;
using Zenject;
using Code.GameModes;

namespace Code.Infrastructure.Installers
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;

        public override void InstallBindings()
        {
            DiInstaller.Install(Container);
            SceneLoadingInstaller.Install(Container, _curtainPrefab);
            GameModeInstaller.Install(Container);
        }
    }
}