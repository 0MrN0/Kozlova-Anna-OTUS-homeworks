using System;
using UnityEngine;
using Code.Core.Data;
using Code.Infrastructure.DI.ModeDI;
using Code.Infrastructure.SceneLoad;

namespace Code.GameModes
{
    public sealed class MainMenuMode : IGameMode, IGameStartRequester
    {
        public event Action SwitchToBattleRequested;

        private readonly ISceneLoader _sceneLoader;
        private readonly IModeDiService _modeDi;
        private readonly ILoadingCurtain _curtain;

        public MainMenuMode(ISceneLoader sceneLoader, IModeDiService modeDi, ILoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _modeDi = modeDi;
            _curtain = curtain;
        }

        public void Enter()
        {
            _sceneLoader.Load((int)GameScene.MainMenu, OnLoaded);
            _modeDi.WarmUp();
        }

        private void OnLoaded()
        {
            _curtain.Hide();
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