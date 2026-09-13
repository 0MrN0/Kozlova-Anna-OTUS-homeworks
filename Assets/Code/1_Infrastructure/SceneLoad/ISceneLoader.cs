using System;

namespace Code.Infrastructure.SceneLoad
{
  public interface ISceneLoader
  {
    public void Load(string name, Action OnLoaded = null);
    public void Load(int index, Action OnLoaded = null);
  }
}