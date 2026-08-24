using R3;

namespace Lessons.Architecture.PM
{
    public interface IStatViewModel : IViewModel
    {
        public ReadOnlyReactiveProperty<string> StatString { get; }
    }
}