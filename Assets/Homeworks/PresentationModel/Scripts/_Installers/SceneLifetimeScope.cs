using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lessons.Architecture.PM
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [Header("Level")]
        [SerializeField] private LevelHelper _levelHelper;
        [SerializeField] private LevelStringFormat _levelStringFormat;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevel(builder);
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelStringFormat);
            builder.Register<LevelViewModelFactory>(Lifetime.Singleton);
            builder.RegisterComponent(_levelHelper);
        }
    }
}