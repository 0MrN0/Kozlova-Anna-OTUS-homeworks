using System;
using Code.Gameplay.Player.View;
using Code.Infrastructure.Inputs;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player
{
    public sealed class PlayerMover : IInitializable, IDisposable, ITickable, IPlayerMover
    {
        private readonly IPlayerMoverView _moverView;
        private readonly IInputService _inputService;
        private readonly PlayerConfig _config;

        private Vector3 _direction;

        public event Action<Vector2, float> DirectionChanged;

        public PlayerMover(IPlayerMoverView moverView, IInputService inputService, PlayerConfig config)
        {
            _moverView = moverView;
            _inputService = inputService;
            _config = config;
        }

        public void Initialize()
        {
            _inputService.MoveKeyPressed += MoveKeyPressed;
            _inputService.MoveKeyReleased += MoveKeyReleased;
        }

        private void MoveKeyReleased()
        {
            _direction = Vector3.zero;
            DirectionChanged?.Invoke(_direction, 0f);
        }

        private void MoveKeyPressed(Vector2 direction)
        {
            _direction = direction * _config.MoveSpeed;
            DirectionChanged?.Invoke(direction, _direction.magnitude);
        }

        public void Dispose()
        {
            _inputService.MoveKeyPressed -= MoveKeyPressed;
            _inputService.MoveKeyReleased -= MoveKeyReleased;
        }

        public void Tick()
        {
            _moverView.Move(_direction * Time.deltaTime);
        }
    }
}