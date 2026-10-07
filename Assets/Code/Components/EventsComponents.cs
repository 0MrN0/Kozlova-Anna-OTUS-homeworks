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

    [Serializable]
    public struct DamageEvent
    {
        public EcsPackedEntity Target;
        public int Amount;
    }
}