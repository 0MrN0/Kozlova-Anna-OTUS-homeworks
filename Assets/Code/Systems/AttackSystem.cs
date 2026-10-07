using Code.Components;
using Code.Configs;
using Code.Data;
using Code.Services.StaticTools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Code.Systems
{
    public sealed class AttackSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsWorldInject _events = EcsWorlds.EVENTS;

        private readonly EcsFilterInject<Inc<AttackTarget, AttackCooldown, Position, Team>, Exc<Dead>> _filter;

        private readonly EcsCustomInject<BulletConfig> _bulletConfig;

        public void Run(EcsSystems systems)
        {
            var deltaTime = Time.deltaTime;

            var targetPool = _filter.Pools.Inc1;
            var cooldownPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;
            var teamPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                ref var cooldown = ref cooldownPool.Get(entity);
                cooldown.Timer -= deltaTime;

                if (cooldown.Timer <= 0)
                {
                    var packedTarget = targetPool.Get(entity).Value;
                    if (!packedTarget.Unpack(_world.Value, out var target)
                            || !positionPool.Has(target)) continue;

                    var attackerPosition = positionPool.Get(entity).Value;
                    var direction = positionPool.Get(target).Value - attackerPosition;
                    direction.y = 0f;
                    attackerPosition += direction.normalized * _bulletConfig.Value.ShotOffset;
                    if (direction.sqrMagnitude < 0.0001f) continue;
                    var rotation = Quaternion.LookRotation(direction);

                    var request = _events.Value.SendSpawnRequest(_bulletConfig.Value.Prefab, attackerPosition, rotation);
                    _events.Value.SetTeam(request, teamPool.Get(entity).Value);

                    cooldown.Timer = cooldown.Duration;
                }
            }
        }
    }
}