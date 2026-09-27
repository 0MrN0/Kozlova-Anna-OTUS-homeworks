using System;
using UnityEngine;

namespace Code.Services.Inputs
{
    public interface IInputManager : IDisposable
    {
        bool SpawnRedCubePressed { get; }
        bool SpawnBlueCubePressed { get; }
        bool CameraRotateHeld { get; }
        Vector2 PointerPosition { get; }
        Vector2 CameraMove { get; }
        Vector2 LookDelta { get; }
        float Zoom { get; }

        void Enable();
        void Disable();
    }
}