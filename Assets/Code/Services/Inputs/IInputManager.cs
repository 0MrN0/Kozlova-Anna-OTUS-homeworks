using System;
using UnityEngine;

namespace Code.Services.Inputs
{
    public interface IInputManager : IDisposable
    {
        public bool SpawnRedCubePressed { get; }
        public bool SpawnBlueCubePressed { get; }
        public Vector2 PointerPosition { get; }

        public void Enable();
        public void Disable();
    }
}