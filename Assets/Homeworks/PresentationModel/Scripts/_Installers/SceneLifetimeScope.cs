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

        [Header("Stats")]
        [SerializeField] private PlayerStatsHelper _playerStatsHelper;
        [SerializeField] private StatStringFormat _statStringFormat;

        [Header("User")]
        [SerializeField] private UserHelper _userHelper;
        [SerializeField] private NameStringFormat _nameStringFormat;
        [SerializeField] private DescriptionStringFormat _descriptionStringFormat;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureLevel(builder);
            ConfigureStats(builder);
            ConfigureUser(builder);
        }

        private void ConfigureLevel(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelStringFormat);
            builder.Register<LevelViewModelFactory>(Lifetime.Singleton);
            builder.RegisterComponent(_levelHelper);
        }

        private void ConfigureStats(IContainerBuilder builder)
        {
            builder.RegisterInstance(_statStringFormat);
            builder.Register<StatViewModelFactory>(Lifetime.Singleton);
            builder.RegisterComponent(_playerStatsHelper);
        }

        private void ConfigureUser(IContainerBuilder builder)
        {
            builder.RegisterInstance(_nameStringFormat);
            builder.RegisterInstance(_descriptionStringFormat);
            builder.Register<UserViewModelFactory>(Lifetime.Singleton);
            builder.RegisterComponent(_userHelper);
        }
    }
}