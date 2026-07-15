using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathAgent : MonoBehaviour, ISceneCyclePreStart, ISceneCycleOnDestroy
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CharacterComponentsHolder componentsHolder;

        public void OnPreStart()
        {
            componentsHolder.HpComponent.HpEmptyEvent += OnCharacterDeath;
        }

        public void OnOnDestroy()
        {
            componentsHolder.HpComponent.HpEmptyEvent -= OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            gameManager.FinishGame();
        }
    }
}