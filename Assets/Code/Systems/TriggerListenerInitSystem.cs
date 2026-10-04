using Code.Components;
using Code.Services.Physics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class TriggerListenerInitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TriggerListenerView>, Exc<TriggerListenerInited>> _filter;
        private readonly EcsPoolInject<TriggerListenerInited> _initedPool;
        private readonly EcsCustomInject<ITriggerEventSink> _sink;

        public void Run(EcsSystems systems)
        {
            var viewPool = _filter.Pools.Inc1;
            var initedPool = _initedPool.Value;

            foreach (var entity in _filter.Value)
            {
                var view = viewPool.Get(entity);
                view.Value.Init(_sink.Value);
                initedPool.Add(entity);
            }
        }
    }
}