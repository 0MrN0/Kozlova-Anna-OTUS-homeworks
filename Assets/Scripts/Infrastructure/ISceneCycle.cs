namespace ShootEmUp
{
    public interface ISceneCycle { }

    public interface ISceneCycleAwake : ISceneCycle { public void OnAwake(); }

    public interface ISceneCyclePreStart : ISceneCycle { public void OnPreStart(); }

    public interface ISceneCycleStart : ISceneCycle { public void OnStart(); }

    public interface ISceneCycleFixedUpdate : ISceneCycle { public void OnFixedUpdate(); }

    public interface ISceneCycleUpdate : ISceneCycle { public void OnUpdate(); }

    public interface ISceneCycleLateUpdate : ISceneCycle { public void OnLateUpdate(); }

    public interface ISceneCycleOnDestroy : ISceneCycle { public void OnOnDestroy(); }

    public interface ISceneCyclePause : ISceneCycle { public void OnPause(); }

    public interface ISceneCycleResume : ISceneCycle { public void OnResume(); }
}