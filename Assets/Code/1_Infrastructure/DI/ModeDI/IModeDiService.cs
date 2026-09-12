using Zenject;

namespace Code.Infrastructure.DI.ModeDI
{
    public interface IModeDiService
    {
        DiContainer Container { get; set; }
        void WarmUp();
        void CleanUp();
    }
}
