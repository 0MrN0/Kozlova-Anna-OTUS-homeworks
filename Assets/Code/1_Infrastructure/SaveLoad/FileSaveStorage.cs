using System.IO;
using UnityEngine;

namespace Code.Infrastructure.SaveLoad
{
    public sealed class FileSaveStorage : ISaveStorage
    {
        private readonly string _directory;

        public FileSaveStorage()
        {
            _directory = Application.persistentDataPath;
        }

        public void Delete(string key)
        {
            var path = GetPath(key);
            if (File.Exists(path)) File.Delete(path);
        }

        public string Read(string key)
        {
            var path = GetPath(key);
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }

        public void Write(string key, string value)
        {
            var path = GetPath(key);
            var tempPath = $"{path}.tmp";

            File.WriteAllText(tempPath, value);

            if (File.Exists(path))
            {
                File.Replace(tempPath, path, null);
            }
            else
            {
                File.Move(tempPath, path);
            }
        }

        private string GetPath(string key) => Path.Combine(_directory, $"{key}.json");
    }
}
