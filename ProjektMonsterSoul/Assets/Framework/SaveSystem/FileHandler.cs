using System.IO;

namespace Framework.SaveSystem
{
    public class FileHandler
    {
        public string FilePath;
        public string Content;

        public FileHandler(string filePath, string content = null)
        {
            FilePath = filePath;
            Content = content;
        }

        public bool Exists()
        {
            return File.Exists(FilePath);
        }

        public void Save()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            using FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            using StreamWriter writer = new StreamWriter(stream);
            writer.Write(Content);
        }

        public void Load()
        {
            if (!File.Exists(FilePath)) return;
            
            using FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate);
            using StreamReader reader = new StreamReader(stream);
            Content = reader.ReadToEnd();
        }

        public void Delete()
        {
            if (!File.Exists(FilePath)) return;
            
            File.Delete(FilePath);
        }
    }
}