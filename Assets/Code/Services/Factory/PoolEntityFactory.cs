using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Code.Services.Factory
{
    public sealed class PoolEntityFactory : IEntityFactory
    {
        private readonly EcsWorld _world;
        private readonly Transform _container;

        private readonly Dictionary<Entity, Stack<Entity>> _pools = new();
        private readonly Dictionary<Entity, Entity> _prefabOf = new();
        private readonly Dictionary<int, Entity> _active = new();
        private readonly Dictionary<Entity, IPoolable[]> _poolables = new();

        public PoolEntityFactory(EcsWorld world, Transform container)
        {
            _world = world;
            _container = container;
        }

        public Entity Spawn(Entity prefab, Vector3 position, Quaternion rotation)
        {
            Entity instance = TakeOrCreate(prefab);
            instance.transform.SetParent(null, false);
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.gameObject.SetActive(true);
            NotifySpawned(instance);

            instance.Initialize(_world);
            _active.Add(instance.Id, instance);
            return instance;

        }

        public void Despawn(int entity)
        {
            if (!_active.Remove(entity, out var instance))
            {
                return;
            }

            instance.Dispose();
            NotifyDespawned(instance);
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(_container, false);

            if (_prefabOf.TryGetValue(instance, out var prefab))
            {
                var pool = GetPool(prefab);
                pool.Push(instance);
            }
            else
            {
                Object.Destroy(instance.gameObject);
            }
        }

        public void Prewarm(Entity prefab, int count)
        {
            if (_pools.ContainsKey(prefab)) return;

            var pool = new Stack<Entity>();
            for (var i = 0; i < count; i++)
            {
                var entity = CreateInstance(prefab);
                pool.Push(entity);
            }

            _pools.Add(prefab, pool);
        }

        public void RegisterSceneEntities()
        {
            var entities = Object.FindObjectsByType<Entity>(FindObjectsSortMode.None);
            foreach (var entity in entities)
            {
                entity.Initialize(_world);
                _active.Add(entity.Id, entity);
            }
        }

        public string GetStats()
        {
            var builder = new System.Text.StringBuilder();
            builder.AppendLine($"Active: {_active.Count}, Total created: {_prefabOf.Count}");

            foreach (var pair in _pools)
                builder.AppendLine($"{pair.Key.name}: free {pair.Value.Count}");

            return builder.ToString();
        }


        private Entity TakeOrCreate(Entity prefab)
        {
            var pool = GetPool(prefab);
            if (pool.TryPop(out var existEntity))
            {
                return existEntity;
            }

            var newEntity = CreateInstance(prefab);
            return newEntity;
        }

        private Entity CreateInstance(Entity prefab)
        {
            var instance = Object.Instantiate(prefab, _container);
            _prefabOf.Add(instance, prefab);
            _poolables.Add(instance, instance.GetComponentsInChildren<IPoolable>(true));
            return instance;
        }

        private Stack<Entity> GetPool(Entity prefab)
        {
            if (!_pools.TryGetValue(prefab, out var pool))
            {
                pool = new Stack<Entity>();
                _pools.Add(prefab, pool);
            }
            return pool;
        }

        private void NotifySpawned(Entity instance)
        {
            if (!_poolables.TryGetValue(instance, out var poolables))
            {
                return;
            }

            foreach (var p in poolables)
            {
                p.OnSpawned();
            }
        }

        private void NotifyDespawned(Entity instance)
        {
            if (!_poolables.TryGetValue(instance, out var poolables))
            {
                return;
            }

            foreach (var p in poolables)
            {
                p.OnDespawned();
            }
        }
    }
}