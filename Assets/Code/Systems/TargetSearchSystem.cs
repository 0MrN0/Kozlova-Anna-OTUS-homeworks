using Code.Components;
using Code.Configs;
using Code.Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Systems
{
    public sealed class TargetSearchSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<SearchTarget, Position, VisionRadius, Team, Health>, Exc<Dead>> _filter;
        private readonly EcsPoolInject<AttackTarget> _targetPool;
        private readonly EcsPoolInject<Dead> _deadPool;
        private readonly EcsCustomInject<LayerMaskConfig> _maskConfig;

        private readonly Collider[] _nearestEnemies = new Collider[200];
        private int _bodyMask;

        public void Init(EcsSystems systems)
        {
            _bodyMask = _maskConfig.Value.BodyMask.value;
        }

        public void Run(EcsSystems systems)
        {
            var searchPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var radiusPool = _filter.Pools.Inc3;
            var teamPool = _filter.Pools.Inc4;
            var healthPool = _filter.Pools.Inc5;
            var targetPool = _targetPool.Value;
            var deadPool = _deadPool.Value;

            foreach (var entity in _filter.Value)
            {
                var position = positionPool.Get(entity).Value;
                var radius = radiusPool.Get(entity).Value;
                var team = teamPool.Get(entity);

                var count = Physics.OverlapSphereNonAlloc(position, radius, _nearestEnemies, _bodyMask);

                for (var i = 0; i < count; i++)
                {
                    var enemyEntity = _nearestEnemies[i].GetComponentInParent<Entity>();
                    if (enemyEntity == null || !enemyEntity.IsAlive()) continue;

                    if (deadPool.Has(enemyEntity.Id)) continue;
                    if (!healthPool.Has(enemyEntity.Id)) continue;
                    if (!teamPool.Has(enemyEntity.Id)) continue;

                    if (team.Value == teamPool.Get(enemyEntity.Id).Value) continue;

                    targetPool.Add(entity).Value = _world.Value.PackEntity(enemyEntity.Id);
                    break;
                }

                searchPool.Del(entity);
            }
        }
    }
}