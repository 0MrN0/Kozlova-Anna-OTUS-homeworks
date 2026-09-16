using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Inputs
{

    public sealed class InputService : IInputService, IDisposable
    {
        public InputActionAsset Actions => _actions.asset;
        public DefaultInputActions.UIActions UI => _actions.UI;
        
        public event Action<Vector2> MoveRequested;
        public event Action ClickRequested;

        private readonly DefaultInputActions _actions;

        public InputService()
        {
            _actions = new();
            Subscribe();
        }

        public void SwitchToGameplayInputMap()
        {
            _actions.UI.Disable();
            _actions.Player.Enable();
        }

        public void SwitchToUiInputMap()
        {
            _actions.Player.Disable();
            _actions.UI.Enable();
        }

        public void Dispose()
        {
            _actions.Dispose();
        }

        private void Subscribe()
        {
            _actions.Player.Move.performed += MovePerformer;
            _actions.UI.Click.performed += ClickPerformer;
        }

        private void ClickPerformer(InputAction.CallbackContext context)
        {
            ClickRequested?.Invoke();
        }

        private void MovePerformer(InputAction.CallbackContext ctx)
        {
            MoveRequested?.Invoke(ctx.ReadValue<Vector2>());
        }
    }
}