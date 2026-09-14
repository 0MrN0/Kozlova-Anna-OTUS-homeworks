using Code.Infrastructure.Inputs.View;
using UnityEngine.InputSystem.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public sealed class SceneInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputSystemUIInputModule>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystemUIBinder>().AsSingle().NonLazy();
        }
    }
}