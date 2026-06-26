using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public class CharacterMoveAgent : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;

        private MoveComponent _moveComponent;
        private float _horizontalDir;

        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

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
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDir, 0) * Time.fixedDeltaTime);
        }

        private void OnHorizontalDirectionChanged(float newDir)
        {
            _horizontalDir = newDir;
        }
    }
}