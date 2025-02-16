using System;
using Framework.SaveSystem.Data;

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