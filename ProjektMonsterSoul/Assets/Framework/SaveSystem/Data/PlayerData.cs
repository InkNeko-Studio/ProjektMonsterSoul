using System;
using Framework.AreaSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class PlayerData
    {
        public string name = "Player";
        [JsonConverter(typeof(ColorData))]public Color skinColor = Color.white;
        public Area area = Area.Chest;
        public float positionX = 0f;
        public float positionY = 0f;
    }
}