using System;
using UnityEngine;
using Code.Core.Data;
using Code.Infrastructure.DI.ModeDI;
using Code.Infrastructure.SceneLoad;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Inputs;

namespace Code.GameModes
{
    public sealed class MainMenuMode : IGameMode, IGameStartRequester
    {
        public event Action SwitchToBattleRequested;

        private readonly ISceneLoader _sceneLoader;
        private readonly IInputService _input;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;

        public MainMenuMode(ISceneLoader sceneLoader, IModeDiService modeDi, ILoadingCurtain curtain, IInputService input)
        {
            _sceneLoader = sceneLoader;
            _input = input;
            _modeDi = modeDi;
            _curtain = curtain;
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
            _input.SwitchToUiInputMap();
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
            // saveload.NewFile()
            Debug.Log("New save file created");
            SwitchToBattleRequested?.Invoke();
        }

        public void RequestLoadGame()
        {
            // saveload.Load()
            Debug.Log("Save file loaded");
            SwitchToBattleRequested?.Invoke();
        }
    }
}