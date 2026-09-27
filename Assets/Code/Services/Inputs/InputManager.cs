using UnityEngine;

namespace Code.Services.Inputs
{
    public sealed class InputManager : IInputManager
    {
        public bool SpawnRedCubePressed => _inputActions.Gameplay.SpawnRedCube.WasPerformedThisFrame();
        public bool SpawnBlueCubePressed => _inputActions.Gameplay.SpawnBlueCube.WasPerformedThisFrame();
        public bool CameraRotateHeld => _inputActions.Gameplay.CameraRotateHold.IsPressed();
        public float Zoom => _inputActions.Gameplay.Zoom.ReadValue<float>();
        public Vector2 PointerPosition => _inputActions.Gameplay.Point.ReadValue<Vector2>();
        public Vector2 CameraMove => _inputActions.Gameplay.CameraMove.ReadValue<Vector2>();
        public Vector2 LookDelta => _inputActions.Gameplay.LookDelta.ReadValue<Vector2>();

        private readonly InputActions _inputActions;

        public InputManager()
        {
            _inputActions = new();
        }

        public void Dispose()
        {
            Disable();
            _inputActions.Dispose();
        }

        public void Enable()
        {
            _inputActions.Gameplay.Enable();
        }

        public void Disable()
        {
            _inputActions.Gameplay.Disable();
        }
    }
}