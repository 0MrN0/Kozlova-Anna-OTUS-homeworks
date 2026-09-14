using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.SceneLoad
{
   public class SceneLoader : ISceneLoader
   {
      public async UniTask Load(string name)
      {
         AsyncOperation operation = SceneManager.LoadSceneAsync(name);

         if (operation == null)
         {
            Debug.LogError($"[SceneLoader] Сцены '{name}' нет в Build Settings — загрузка невозможна");
            return;
         }

         await operation.ToUniTask();
      }

      public async UniTask Load(int index)
      {
         AsyncOperation operation = SceneManager.LoadSceneAsync(index);

         if (operation == null)
         {
            Debug.LogError($"[SceneLoader] Индекса сцены '{index}' нет в Build Settings — загрузка невозможна");
            return;
         }

         await operation.ToUniTask();
      }
   }
}
