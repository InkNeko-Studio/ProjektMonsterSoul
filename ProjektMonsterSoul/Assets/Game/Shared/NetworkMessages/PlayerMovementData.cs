using System;
using Game.Shared.NetworkMessages;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    [Serializable]
    public class PlayerMovementData : MessageBaseData
    {
        public string playerName;

        public float time;
        
        public float positionX, positionY;
        public float speedX, speedY;

        public PlayerMovementData()
        {
            this.networkTag = NetworkTag.PlayerMovement;
        }
    }
}