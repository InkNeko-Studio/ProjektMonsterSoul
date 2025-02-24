using System;
using Framework.SaveSystem.Data;
using Game.Shared.NetworkMessages;

namespace Game.Shared.Player.Scripts
{
    [Serializable]
    public class PlayerStartInfoData : MessageBaseData
    {
        public PlayerData playerData;

        public PlayerStartInfoData()
        {
            this.networkTag = NetworkTag.PlayerStartInfo;
        }
    }
}