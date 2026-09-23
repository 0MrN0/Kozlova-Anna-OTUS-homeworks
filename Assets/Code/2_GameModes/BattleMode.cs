using Code.Core.Data;
using Code.Infrastructure.SceneLoad;
using Code.Infrastructure.DI.ModeDI;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Inputs;
using System;
using Code.Infrastructure.SaveLoad;

namespace Code.GameModes
{
    public sealed class BattleMode : IGameMode, IExitToMenuRequester
    {
        public event Action BattleExitRequested;

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _input;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public BattleMode(ISceneLoader sceneLoader, IModeDiService localDI, ILoadingCurtain curtain, IInputService input, ISaveLoadAggregate saveLoadAggregate)
        {
            _sceneLoader = sceneLoader;
            _input = input;
            _modeDi = localDI;
            _curtain = curtain;
            _saveLoadAggregate = saveLoadAggregate;
        }

        public void Enter()
        {
            EnterAsync().Forget();
        }

        private async UniTaskVoid EnterAsync()
        {
            _saveLoadAggregate.ReadProgress();
            await _sceneLoader.Load((int)GameScene.Battle);
            _modeDi.WarmUp();
            _curtain.Hide().Forget();
            _input.EnablePlayerInputMap();
        }

        public void Exit()
        {
            _saveLoadAggregate.Cleanup();
            _modeDi.CleanUp();
            _curtain.Show();
        }

        public void Tick()
        {

        }

        public void RequestExitToMenu()
        {
            _saveLoadAggregate.Save();
            BattleExitRequested?.Invoke();
        }
    }
}
