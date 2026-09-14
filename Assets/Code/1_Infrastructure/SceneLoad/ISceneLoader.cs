using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.SceneLoad
{
  public interface ISceneLoader
  {
    public UniTask Load(string name);
    public UniTask Load(int index);
  }
}