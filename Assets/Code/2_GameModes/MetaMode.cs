using Code.Infrastructure.DI.ModeDI;
using Code.Infrastructure.SceneLoad;

namespace Code.GameModes
{
    public class MetaMode : IGameMode
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IModeDiService _localDI;
        private readonly ILoadingCurtain _curtain;

        public MetaMode(ISceneLoader sceneLoader, IModeDiService localDI, ILoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _localDI = localDI;
            _curtain = curtain;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Tick()
        {

        }
    }
}
