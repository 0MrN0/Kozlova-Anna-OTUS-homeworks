using System;
using R3;

namespace Lessons.Architecture.PM
{
    public interface ILevelViewModel : IViewModel, IDisposable
    {
        public ReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        public ReadOnlyReactiveProperty<string> LevelString { get; }
        public ReadOnlyReactiveProperty<string> ExperienceString { get; }
        public ReadOnlyReactiveProperty<float> ExperienceProgress { get; }

        public void LevelUp();
    }
}