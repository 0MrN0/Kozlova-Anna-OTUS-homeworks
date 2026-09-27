using UnityEngine;

namespace Code.Services.Inputs
{
    public sealed class InputManager : IInputManager
    {
        public bool SpawnRedCubePressed => _inputActions.Gameplay.SpawnRedCube.WasPerformedThisFrame();
        public bool SpawnBlueCubePressed => _inputActions.Gameplay.SpawnBlueCube.WasPerformedThisFrame();
        public Vector2 PointerPosition => _inputActions.Gameplay.Point.ReadValue<Vector2>();

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