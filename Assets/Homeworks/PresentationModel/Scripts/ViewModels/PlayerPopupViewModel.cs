namespace Lessons.Architecture.PM
{
    public sealed class PlayerPopupViewModel : IPlayerPopupViewModel
    {
        public IUserViewModel UserViewModel { get; private set; }
        public IPlayerStatsViewModel StatsViewModel { get; private set; }
        public ILevelViewModel LevelViewModel { get; private set; }

        public PlayerPopupViewModel(IUserViewModel userViewModel, IPlayerStatsViewModel statsViewModel, ILevelViewModel levelViewModel)
        {
            UserViewModel = userViewModel;
            StatsViewModel = statsViewModel;
            LevelViewModel = levelViewModel;
        }

        public void Dispose()
        {
            UserViewModel.Dispose();
            StatsViewModel.Dispose();
            LevelViewModel.Dispose();
        }
    }
}