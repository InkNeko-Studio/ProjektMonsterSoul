using System;
using Game.Shared.Player.Scripts;

namespace Game.Shared.NetworkMessages
{
    [Serializable]
    public class MessageBaseData
    {
        public NetworkTag networkTag = NetworkTag.None;
    }
}