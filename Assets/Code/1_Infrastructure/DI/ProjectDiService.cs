using System.Collections.Generic;
using Zenject;

namespace Code.Infrastructure.DI
{
   public class ProjectDiService : IProjectDiService
   {
      public DiContainer Container { get; private set; }

      public ProjectDiService()
      {
         Container = ProjectContext.Instance.Container;
      }

      public IEnumerable<T> ResolveAll<T>() => Container.ResolveAll<T>();
   }
}