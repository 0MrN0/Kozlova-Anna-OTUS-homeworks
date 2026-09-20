using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Inputs
{
    public interface IInputService
    {
        InputActionAsset Actions { get; }
        InputActions.UIActions UI { get; }

        public event Action<Vector2> MoveKeyPressed;
        public event Action MoveKeyReleased;

        public void DisablePlayerInputMap();
        public void EnablePlayerInputMap();
    }
}