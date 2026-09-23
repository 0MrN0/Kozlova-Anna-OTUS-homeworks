using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Data
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public Vector2 PlayerPosition;
        public long Score;
        public List<FlowerProgress> Flowers = new();
    }

    [Serializable]
    public sealed class FlowerProgress
    {
        public string ConfigId;
        public Vector2 Position;
    }
}
