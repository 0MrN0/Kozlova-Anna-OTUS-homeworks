using UnityEngine;
using Zenject;

namespace Code.Infrastructure.DI.ModeDI
{
   /// <summary>
   /// Локальный контейнер режима: дочерний от проектного.
   /// WarmUp() создаёт его заново на входе в режим, CleanUp() выбрасывает на выходе —
   /// так локальные зависимости живут ровно столько, сколько живёт режим,
   /// а проектные (сейвы, прогресс) переживают все переключения.
   /// </summary>
   public class ModeDiService : IModeDiService
   {
      private readonly IProjectDiService _di;

      public DiContainer Container { get; set; }

      public ModeDiService(IProjectDiService di)
      {
         _di = di;
         Container = new DiContainer(_di.Container);
      }

      public void WarmUp()
      {
         Debug.Log("[LocalDI] WarmUp — новый дочерний контейнер режима");
         Container = new DiContainer(_di.Container);
      }

      public void CleanUp()
      {
         Debug.Log("[LocalDI] CleanUp — контейнер режима выброшен");
         Container = new DiContainer();
      }
   }
}
