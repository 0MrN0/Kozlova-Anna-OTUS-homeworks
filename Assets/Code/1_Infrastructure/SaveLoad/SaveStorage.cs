using UnityEngine;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class SaveStorage : ISaveStorage
    {
        public string Read(string key) => PlayerPrefs.GetString(key, null);

        public void Write(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }
    }
}
