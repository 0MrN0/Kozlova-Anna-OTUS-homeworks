using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lessons.Architecture.PM
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerHelper _playerHelper;

        [Header("Formats")]
        [SerializeField] private LevelStringFormat _levelStringFormat;
        [SerializeField] private StatStringFormat _statStringFormat;
        [SerializeField] private NameStringFormat _nameStringFormat;
        [SerializeField] private DescriptionStringFormat _descriptionStringFormat;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevel(builder);
            ConfigureStats(builder);
            ConfigureUser(builder);
            builder.RegisterComponent(_playerHelper);
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelStringFormat);
            builder.Register<LevelViewModelFactory>(Lifetime.Singleton);
        }

        private void ConfigureStats(IContainerBuilder builder)
        {
            builder.RegisterInstance(_statStringFormat);
            builder.Register<StatViewModelFactory>(Lifetime.Singleton);
        }

        private void ConfigureUser(IContainerBuilder builder)
        {
            builder.RegisterInstance(_nameStringFormat);
            builder.RegisterInstance(_descriptionStringFormat);
            builder.Register<UserViewModelFactory>(Lifetime.Singleton);
        }
    }
}