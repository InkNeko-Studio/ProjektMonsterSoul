using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class PlayerData
    {
        public string name = "Player";
        [JsonConverter(typeof(ColorData))]public Color skinColor = Color.white;
    }
}