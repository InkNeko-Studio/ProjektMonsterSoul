using System;
using Framework.SaveSystem.Data;

namespace Framework.SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public int saveTime = 0;
        public PlayerData playerData = new();
    }
}