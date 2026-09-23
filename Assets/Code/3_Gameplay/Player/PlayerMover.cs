using System;
using Code.Core.Contracts;
using Code.Core.Data;
using Code.Gameplay.Player.View;
using Code.Infrastructure.Inputs;
using Code.Infrastructure.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player
{
    public sealed class PlayerMover : IInitializable, IDisposable, ITickable, IPlayerMover, ISaveLoad
    {
        private readonly IPlayerMoverView _moverView;
        private readonly IInputService _inputService;
        private readonly PlayerConfig _config;
        private readonly ISaveLoadAggregate _saveLoadAggregate;

        private Vector3 _direction;

        public event Action<Vector2, float> DirectionChanged;

        public PlayerMover(IPlayerMoverView moverView, IInputService inputService, PlayerConfig config, ISaveLoadAggregate saveLoadAggregate)
        {
            _moverView = moverView;
            _inputService = inputService;
            _config = config;
            _saveLoadAggregate = saveLoadAggregate;

            _saveLoadAggregate.Register(this);
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

        public void Save(PlayerProgress progress)
        {
            progress.PlayerPosition = _moverView.GetPosition();
        }

        public void Load(PlayerProgress progress)
        {
            _moverView.SetPosition(progress.PlayerPosition);
        }
    }
}