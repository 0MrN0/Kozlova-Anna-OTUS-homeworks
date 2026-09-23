using System;
using Code.Core.Data;
using Code.Infrastructure.DI.ModeDI;
using Code.Infrastructure.SceneLoad;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Inputs;
using Code.Infrastructure.SaveLoad;

namespace Code.GameModes
{
    public sealed class MainMenuMode : IGameMode, IGameStartRequester
    {
        public event Action SwitchToBattleRequested;

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _input;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        public MainMenuMode(ISceneLoader sceneLoader, IModeDiService modeDi, ILoadingCurtain curtain, IInputService input, ISaveLoadAggregate saveLoadAggregate)
        {
            _sceneLoader = sceneLoader;
            _input = input;
            _modeDi = modeDi;
            _curtain = curtain;
            _saveLoadAggregate = saveLoadAggregate;
        }

        public void Enter()
        {
            EnterAsync().Forget();
        }

        private async UniTaskVoid EnterAsync()
        {
            await _sceneLoader.Load((int)GameScene.MainMenu);
            _modeDi.WarmUp();
            _curtain.Hide().Forget();
            _input.DisablePlayerInputMap();
        }

        public void Exit()
        {
            _modeDi.CleanUp();
            _curtain.Show();
        }

        public void Tick()
        {

        }

        public void RequestNewGame()
        {
            _saveLoadAggregate.SetNeedLoadGame(false);
            SwitchToBattleRequested?.Invoke();
        }

        public void RequestLoadGame()
        {
            _saveLoadAggregate.SetNeedLoadGame(true);
            SwitchToBattleRequested?.Invoke();
        }
    }
}