using System;
using UnityEngine;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class PlayerData
    {
        public string name;
        public ColorData skinColor = new ColorData();
    }
}