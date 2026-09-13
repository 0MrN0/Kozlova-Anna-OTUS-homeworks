using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.SceneLoad
{
   /// <summary>
   /// Грузит сцену и дёргает колбэк, когда она уже активна.
   /// Корутина и ICoroutineRunner не нужны: AsyncOperation сам сообщает о завершении
   /// через событие completed, поэтому у загрузчика вообще нет зависимостей.
   /// </summary>
   public class SceneLoader : ISceneLoader
   {
      public void Load(string name, Action OnLoaded = null)
      {
         AsyncOperation operation = SceneManager.LoadSceneAsync(name);

         if (operation == null)
         {
            Debug.LogError($"[SceneLoader] Сцены '{name}' нет в Build Settings — загрузка невозможна");
            return;
         }

         // completed срабатывает после того, как Unity активировала сцену:
         // Awake/OnEnable её объектов уже прошли, SceneContext собрал свой контейнер.
         operation.completed += _ => OnLoaded?.Invoke();
      }

      public void Load(int index, Action OnLoaded = null)
      {
         AsyncOperation operation = SceneManager.LoadSceneAsync(index);

         if (operation == null)
         {
            Debug.LogError($"[SceneLoader] Индекса сцены '{index}' нет в Build Settings — загрузка невозможна");
            return;
         }

         // completed срабатывает после того, как Unity активировала сцену:
         // Awake/OnEnable её объектов уже прошли, SceneContext собрал свой контейнер.
         operation.completed += _ => OnLoaded?.Invoke();
      }
   }
}
