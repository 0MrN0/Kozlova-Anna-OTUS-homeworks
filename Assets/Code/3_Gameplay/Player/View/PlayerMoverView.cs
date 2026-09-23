using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.View
{
    public sealed class PlayerMoverView : MonoBehaviour, IPlayerMoverView
    {
        [SerializeField] private CharacterController _characterController;

        [Inject]
        public void Construct(PlayerSpawnPoint spawnPoint)
        {
            SetPosition(spawnPoint.Position);
        }

        public void Move(Vector3 direction)
        {
            _characterController.Move(direction);
        }

        public Vector2 GetPosition()
        {
            return new(transform.position.x, transform.position.y);
        }

        public void SetPosition(Vector2 playerPosition)
        {
            _characterController.enabled = false;
            transform.position = playerPosition;
            _characterController.enabled = true;
        }
    }
}