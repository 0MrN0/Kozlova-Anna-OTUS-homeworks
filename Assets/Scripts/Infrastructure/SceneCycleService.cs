using System.Collections.Generic;
namespace ShootEmUp
{
    public interface ISceneCycleService
    {
        public void Register(ISceneCycle entity);
        public void AllAwake();
        public void AllPreStart();
        public void AllStart();
        public void AllFixedUpdate();
        public void AllUpdate();
        public void AllLateUpdate();
        public void AllOnDestroy();
    }

    public class SceneCycleService : ISceneCycleService
    {
        private readonly List<ISceneCycleAwake> _awakes = new();
        private readonly List<ISceneCyclePreStart> _preStarts = new();
        private readonly List<ISceneCycleStart> _starts = new();
        private readonly List<ISceneCycleFixedUpdate> _fixedUpdates = new();
        private readonly List<ISceneCycleUpdate> _updates = new();
        private readonly List<ISceneCycleLateUpdate> _lateUpdates = new();
        private readonly List<ISceneCycleOnDestroy> _onDestroies = new();


        public void Register(ISceneCycle entity)
        {
            if (entity is ISceneCycleAwake a && !_awakes.Contains(a)) _awakes.Add(a);
            if (entity is ISceneCyclePreStart oe && !_preStarts.Contains(oe)) _preStarts.Add(oe);
            if (entity is ISceneCycleStart s && !_starts.Contains(s)) _starts.Add(s);
            if (entity is ISceneCycleFixedUpdate fu && !_fixedUpdates.Contains(fu)) _fixedUpdates.Add(fu);
            if (entity is ISceneCycleUpdate u && !_updates.Contains(u)) _updates.Add(u);
            if (entity is ISceneCycleLateUpdate lu && !_lateUpdates.Contains(lu)) _lateUpdates.Add(lu);
            if (entity is ISceneCycleOnDestroy ode && !_onDestroies.Contains(ode)) _onDestroies.Add(ode);
        }

        public void RegisterInRuntime(ISceneCycle entity)
        {

        }

        public void AllAwake()
        {
            foreach (var e in _awakes)
            {
                e.OnAwake();
            }
        }

        public void AllPreStart()
        {
            foreach (var e in _preStarts)
            {
                e.OnPreStart();
            }
        }

        public void AllStart()
        {
            foreach (var e in _starts)
            {
                e.OnStart();
            }
        }

        public void AllFixedUpdate()
        {
            foreach (var e in _fixedUpdates)
            {
                e.OnFixedUpdate();
            }
        }

        public void AllUpdate()
        {
            foreach (var e in _updates)
            {
                e.OnUpdate();
            }
        }

        public void AllLateUpdate()
        {
            foreach (var e in _lateUpdates)
            {
                e.OnLateUpdate();
            }
        }

        public void AllOnDestroy()
        {
            foreach (var e in _onDestroies)
            {
                e.OnOnDestroy();
            }
        }
    }
}