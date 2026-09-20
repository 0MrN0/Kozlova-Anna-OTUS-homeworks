using System;
using UnityEngine;

namespace Code.Gameplay.Player
{
    public interface IPlayerMover
    {
        public event Action<Vector2, float> DirectionChanged;
    }
}