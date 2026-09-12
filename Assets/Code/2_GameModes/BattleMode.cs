using Code.Infrastructure.DI.ModeDI;
using Code.Infrastructure.SceneLoad;
using UnityEngine;

namespace Code.GameModes
{
    public class BattleMode : IGameMode
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IModeDiService _localDI;
        private readonly ILoadingCurtain _curtain;

        private const string SCENE_NAME = "BattleScene";

        public BattleMode(ISceneLoader sceneLoader, IModeDiService localDI, ILoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _localDI = localDI;
            _curtain = curtain;
        }

        public void Enter()
        {
            _sceneLoader.Load(SCENE_NAME, OnLoaded);
            _localDI.WarmUp();
        }

        public void Exit()
        {
            // Dispose everything we want to dispose
            _curtain.Show();
        }

        private void OnLoaded()
        {
            Debug.Log("OnLoaded");
            // в этот момент на сцене на всех монобехах стреляет Awake но ни у кого еще не выстрелил Start. Почему? По опыту препода. В доках совсем нет инфы. НУ ПРОСТО СОВСЕМ БОЖЕ
            // GameFactory.Create();
            // var sceneContainer = GameObject.FindAnyObjectByType<LevelContainer>();
            // и выдаем этот сценКонтайнер всем, кому он нужен
            _curtain.Hide();
        }

        public void Tick()
        {

        }
    }
}
