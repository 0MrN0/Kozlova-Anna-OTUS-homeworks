using System;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class CharacterDeathAgent : IStartable, IDisposable
    {
        private readonly GameManager _gameManager;
        private readonly CharacterComponentsHolder _componentsHolder;

        [Inject]
        public CharacterDeathAgent(GameManager gameManager,
                                   CharacterComponentsHolder componentsHolder)
        {
            _gameManager = gameManager;
            _componentsHolder = componentsHolder;
        }

        public void Start()
        {
            _componentsHolder.HpComponent.HpEmptyEvent += OnCharacterDeath;
        }

        public void Dispose()
        {
            _componentsHolder.HpComponent.HpEmptyEvent -= OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            _gameManager.FinishGame();
        }
    }
}