using Code.Core.Data;
using Code.Infrastructure.SceneLoad;
using Code.Infrastructure.DI.ModeDI;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Inputs;
using System;

namespace Code.GameModes
{
    public class BattleMode : IGameMode, IExitToMenuRequester
    {
        public event Action BattleExitRequested;

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _input;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;

        public BattleMode(ISceneLoader sceneLoader, IModeDiService localDI, ILoadingCurtain curtain, IInputService input)
        {
            _sceneLoader = sceneLoader;
            _input = input;
            _modeDi = localDI;
            _curtain = curtain;
        }

        public void Enter()
        {
            EnterAsync().Forget();
        }

        private async UniTaskVoid EnterAsync()
        {
            await _sceneLoader.Load((int)GameScene.Battle);
            _modeDi.WarmUp();
            _curtain.Hide().Forget();
            _input.EnablePlayerInputMap();
        }

        public void Exit()
        {
            _modeDi.CleanUp();
            _curtain.Show();
        }

        public void Tick()
        {

        }

        public void RequestExitToMenu()
        {
            BattleExitRequested?.Invoke();
        }
    }
}
