using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterDeathAgent : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        private HitPointsComponent _hitPoints;

        private void Awake()
        {
            _hitPoints = GetComponent<HitPointsComponent>();
        }

        private void OnEnable()
        {
            _hitPoints.hpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPoints.hpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => gameManager.FinishGame(); // заземленный аргумент не выглядит хорошо
    }
}