using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Inputs
{
    public interface IInputService
    {
        InputActionAsset Actions { get; }
        DefaultInputActions.UIActions UI { get; }

        public event Action<Vector2> MoveRequested;
        public event Action ClickRequested;

        public void SwitchToUiInputMap();
        public void SwitchToGameplayInputMap();
    }
}