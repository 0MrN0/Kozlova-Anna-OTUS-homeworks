using System;
using UnityEngine;

namespace Code.Gameplay.Flower.View
{
    public interface IFlowerView: IDisposable
    {
        Vector2 Position { get; }

        public event Action Pickupped;
    }
}