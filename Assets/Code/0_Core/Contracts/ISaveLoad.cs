using Code.Core.Data;

namespace Code.Core.Contracts
{
    public interface ISaveLoad
    {
        public void Save(PlayerProgress progress);
        public void Load(PlayerProgress progress);
    }
}
