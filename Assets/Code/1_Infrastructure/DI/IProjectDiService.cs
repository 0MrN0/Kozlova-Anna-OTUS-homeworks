using System.Collections.Generic;
using Zenject;

namespace Code.Infrastructure.DI
{
    public interface IProjectDiService
    {
        IEnumerable<T> ResolveAll<T>();
        DiContainer Container { get; }
    }
}
