using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.View
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerMoverView : MonoBehaviour, IPlayerMoverView
    {
        private CharacterController _characterController;

        [Inject]
        public void Construct(PlayerSpawnPoint spawnPoint)
        {
            transform.position = spawnPoint.Position;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction)
        {
            _characterController.Move(direction);
        }
    }
}