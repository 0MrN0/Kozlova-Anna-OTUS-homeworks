using Code.Core.Data;
using Code.Infrastructure.SceneLoad;
using Code.Infrastructure.DI.ModeDI;
using Cysharp.Threading.Tasks;

namespace Code.GameModes
{
    public class BattleMode : IGameMode
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;

        public BattleMode(ISceneLoader sceneLoader, IModeDiService localDI, ILoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _modeDi = localDI;
            _curtain = curtain;
        }

        public void Enter()
        {
            _sceneLoader.Load((int)GameScene.Battle, OnLoaded);
            _modeDi.WarmUp();
        }

        public void Exit()
        {
            _modeDi.CleanUp();
            _curtain.Show();
        }

        private void OnLoaded()
        {
            _curtain.Hide().Forget();
        }

        public void Tick()
        {

        }
    }
}
