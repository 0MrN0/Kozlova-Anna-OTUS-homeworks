namespace Lessons.Architecture.PM
{
    public interface IPlayerPopupViewModel : IViewModel
    {
        public IUserViewModel UserViewModel { get; }
        public IPlayerStatsViewModel StatsViewModel { get; }
        public ILevelViewModel LevelViewModel { get; }
    }
}