using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathAgent : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CharacterComponentHolder componentsHolder;

        private void OnEnable()
        {
            componentsHolder.HpComponent.hpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            componentsHolder.HpComponent.hpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame(); // заземленный аргумент не выглядит хорошо
    }
}