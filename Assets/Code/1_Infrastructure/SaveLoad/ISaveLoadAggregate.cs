using Code.Core.Contracts;

namespace Code.Infrastructure.SaveLoad
{
    public interface ISaveLoadAggregate
    {
        public bool IsNeedLoadGame { get; }

        public void Save();
        public void Register(ISaveLoad participant);
        public void Unregister(ISaveLoad participant);
        public void Cleanup();
        public void ReadProgress();
        public void ApplyProgress();
        public void SetNeedLoadGame(bool isNeed);
    }
}
