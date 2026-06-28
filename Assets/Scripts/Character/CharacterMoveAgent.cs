using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMoveAgent : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private CharacterComponentsHolder componentHolder;

        private float _horizontalDir;

        private void OnEnable()
        {
            inputManager.HorizontalDirectionChangedEvent += OnHorizontalDirectionChanged;
        }

        private void OnDisable()
        {
            inputManager.HorizontalDirectionChangedEvent -= OnHorizontalDirectionChanged;
        }

        private void FixedUpdate()
        {
            componentHolder.MoveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDir, 0) * Time.fixedDeltaTime);
        }

        private void OnHorizontalDirectionChanged(float newDir)
        {
            _horizontalDir = newDir;
        }
    }
}