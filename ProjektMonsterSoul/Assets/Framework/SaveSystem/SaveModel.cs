using System.IO;
using UnityEngine;

namespace Framework.SaveSystem
{
    public class SaveModel
    {
        public static string RetrieveData(int index)
        {
            string filename = $"save{index}";
            string fullPath = Path.Combine(Application.dataPath, filename);
            FileHandler fileHandler = new FileHandler(fullPath);
            fileHandler.Load();
            return fileHandler.Content;
        }

        public static void SaveData(int index, string content)
        {
            string filename = $"save{index}";
            string fullPath = Path.Combine(Application.dataPath, filename);
            FileHandler fileHandler = new FileHandler(fullPath, content);
            fileHandler.Save();
        }

        public static bool HasData(int index)
        {
            string filename = $"save{index}";
            string fullPath = Path.Combine(Application.dataPath, filename);
            FileHandler fileHandler = new FileHandler(fullPath);
            return fileHandler.Exists();
        }

        public static void EraseData(int index)
        {
            string filename = $"save{index}";
            string fullPath = Path.Combine(Application.dataPath, filename);
            FileHandler fileHandler = new FileHandler(fullPath);
            fileHandler.Delete();
        }
    }
}
