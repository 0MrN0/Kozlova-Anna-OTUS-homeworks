using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Inputs
{

    public sealed class InputService : IInputService, IDisposable
    {
        public InputActionAsset Actions => _actions.asset;
        public InputActions.UIActions UI => _actions.UI;

        public event Action<Vector2> MoveKeyPressed;
        public event Action MoveKeyReleased;

        private readonly InputActions _actions;

        public InputService()
        {
            _actions = new();
            Subscribe();
            _actions.UI.Enable();
        }

        public void EnablePlayerInputMap()
        {
            _actions.Player.Enable();
        }

        public void DisablePlayerInputMap()
        {
            _actions.Player.Disable();
        }

        public void Dispose()
        {
            _actions.Dispose();
        }

        private void Subscribe()
        {
            _actions.Player.Move.performed += MovePerformed;
            _actions.Player.Move.canceled += MoveCanceled;
        }

        private void MoveCanceled(InputAction.CallbackContext context)
        {
            MoveKeyReleased?.Invoke();
        }

        private void MovePerformed(InputAction.CallbackContext ctx)
        {
            MoveKeyPressed?.Invoke(ctx.ReadValue<Vector2>());
        }
    }
}