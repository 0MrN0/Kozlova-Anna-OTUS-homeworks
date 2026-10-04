using Code.Components;
using Code.Configs;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Code.Systems
{
    public sealed class ApplyTeamViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, RendererView>, Exc<TeamViewApplied>> _filter;
        private readonly EcsPoolInject<TeamViewApplied> _appliedPool;
        private readonly EcsCustomInject<TeamConfig> _config;

        public void Run(EcsSystems systems)
        {
            var teamPool = _filter.Pools.Inc1;
            var rendererPool = _filter.Pools.Inc2;
            var appliedPool = _appliedPool.Value;
            var config = _config.Value;

            foreach (var entity in _filter.Value)
            {
                var team = teamPool.Get(entity).Value;
                var renderer = rendererPool.Get(entity).Value;

                renderer.sharedMaterial = config.GetMaterial(team);
                appliedPool.Add(entity);
            }
        }
    }
}