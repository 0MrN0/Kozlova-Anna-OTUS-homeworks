namespace Code.Infrastructure.SaveLoad
{
    public interface ISaveStorage
    {
        public string Read(string key);
        public void Write(string key, string value);
        public void Delete(string key);
    }
}
