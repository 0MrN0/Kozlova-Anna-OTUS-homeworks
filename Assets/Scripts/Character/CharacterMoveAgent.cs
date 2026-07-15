using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterMoveAgent : MonoBehaviour, ISceneCyclePreStart, ISceneCycleFixedUpdate, ISceneCycleOnDestroy
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private CharacterComponentsHolder componentHolder;

        private float _horizontalDir;

        public void OnPreStart()
        {
            inputManager.HorizontalDirectionChangedEvent += OnHorizontalDirectionChanged;
        }
        public void OnOnDestroy()
        {
            inputManager.HorizontalDirectionChangedEvent += OnHorizontalDirectionChanged;
        }

        public void OnFixedUpdate()
        {
            componentHolder.MoveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDir, 0) * Time.fixedDeltaTime);
        }

        private void OnHorizontalDirectionChanged(float newDir)
        {
            _horizontalDir = newDir;
        }
    }
}