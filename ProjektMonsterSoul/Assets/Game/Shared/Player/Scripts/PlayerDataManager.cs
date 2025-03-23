using System;
using System.Collections.Generic;
using Framework.Connection;
using Framework.SaveSystem;
using Framework.SaveSystem.Data;
using Newtonsoft.Json;

namespace Game.Shared.Player.Scripts
{
    public static class PlayerDataManager
    {
        public static List<PlayerData> PlayerDataList;

        public static Action<PlayerData> OnNewPlayer;
        public static Action<PlayerData> OnUpdatePlayer;
        
        public static void Start()
        {
            PlayerDataList = new List<PlayerData>();
            ConnectionManager.OnMessage += OnMessage;
        }

        private static void OnMessage(NetworkTag tag, string message)
        {
            if (tag == NetworkTag.PlayerStartInfo)
            {
                var playerStartInfo = JsonConvert.DeserializeObject<PlayerStartInfoData>(message, ConnectionConfig.JsonSettings);
                int i;
                if ((i = PlayerDataList.FindIndex((p) => p.name == playerStartInfo.playerData.name)) != -1)
                {
                    PlayerDataList[i] = playerStartInfo.playerData;
                    OnUpdatePlayer?.Invoke(playerStartInfo.playerData);
                }
                else
                {
                    SendData();
                    PlayerDataList.Add(playerStartInfo.playerData);
                    OnNewPlayer?.Invoke(playerStartInfo.playerData);
                }
            }
        }
        
        public static void SendData()
        {
            PlayerStartInfoData playerStartInfo = new PlayerStartInfoData();
            playerStartInfo.playerData = SaveController.CurrentSave.playerData;
            ConnectionManager.Send(JsonConvert.SerializeObject(playerStartInfo));
        }
    }
}