using System;
using R3;

namespace Lessons.Architecture.PM
{
    public interface IStatViewModel : IViewModel, IDisposable
    {
        public ReadOnlyReactiveProperty<string> StatString { get; }
    }
}