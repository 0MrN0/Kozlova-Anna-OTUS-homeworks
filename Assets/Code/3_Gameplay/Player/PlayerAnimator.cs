using System;
using UnityEngine;
using Code.Gameplay.Player.View;
using Zenject;

namespace Code.Gameplay.Player
{
    public sealed class PlayerAnimator : IDisposable, IInitializable
    {
        private readonly IPlayerMover _playerMover;
        private readonly IPlayerAnimatorView _animatorView;
        private readonly PlayerConfig _config;

        public PlayerAnimator(IPlayerMover playerMover, IPlayerAnimatorView animatorView, PlayerConfig config)
        {
            _playerMover = playerMover;
            _animatorView = animatorView;
            _config = config;

            _playerMover.DirectionChanged += OnDirectionChanged;
        }

        public void Dispose()
        {
            _playerMover.DirectionChanged -= OnDirectionChanged;
        }

        public void Initialize()
        {
            _animatorView.SetAnimationSpeed(_config.IdleAnimationSpeed);
        }

        private void OnDirectionChanged(Vector2 direction, float speed)
        {
            _animatorView.SetMoveX(direction.x);
            _animatorView.SetMoveY(direction.y);

            _animatorView.SetAnimationSpeed(speed > 0f
                                                ? _config.MoveAnimationSpeed
                                                : _config.IdleAnimationSpeed);
        }
    }
}