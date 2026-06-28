using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathAgent : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CharacterComponentHolder componentsHolder;

        private void OnEnable()
        {
            componentsHolder.HpComponent.HpEmptyEvent += OnCharacterDeath;
        }

        private void OnDisable()
        {
            componentsHolder.HpComponent.HpEmptyEvent -= OnCharacterDeath;
        }

        private void OnCharacterDeath() => gameManager.FinishGame();
    }
}