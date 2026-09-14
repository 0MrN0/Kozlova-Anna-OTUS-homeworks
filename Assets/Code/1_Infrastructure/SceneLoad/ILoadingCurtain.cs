using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.SceneLoad
{
    public interface ILoadingCurtain
    {
        void Show();
        UniTask Hide();
    }
}
