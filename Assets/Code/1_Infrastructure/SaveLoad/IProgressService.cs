using Code.Core.Data;

namespace Code.Infrastructure.SaveLoad
{
    public interface IProgressService
    {
        public PlayerProgress Progress { get; }
        public PlayerProgress CreateNewProgress();
        public string ToJson();
    }
}
