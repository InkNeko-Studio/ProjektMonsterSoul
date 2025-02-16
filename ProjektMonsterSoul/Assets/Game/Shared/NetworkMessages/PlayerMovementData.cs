using System;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    [Serializable]
    public class PlayerMovementData : MessageBaseData
    {
        public int playerId;

        public float time;
        
        public float positionX, positionY;
        public float speedX, speedY;

        public PlayerMovementData()
        {
            this.networkTag = NetworkTag.PlayerMovement;
        }
    }
}