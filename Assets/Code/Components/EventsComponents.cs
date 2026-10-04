using System;
using Leopotam.EcsLite;

namespace Code.Components
{
    [Serializable]
    public struct TriggerEnterEvent
    {
        public EcsPackedEntity Owner;
        public EcsPackedEntity Other;
    }
}