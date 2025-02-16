using System;
using Framework.Connection;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerSkinColor : MonoBehaviour
    {
        private SpriteRenderer _playerSprite;
        
        private void Start()
        {
            _playerSprite = GetComponentInChildren<SpriteRenderer>();
            ConnectionManager.OnMessage += OnMessage;
        }

        private void OnMessage(NetworkTag tag, string message)
        {
            if (tag == NetworkTag.PlayerStartInfo)
            {
                var playerStartInfo = JsonConvert.DeserializeObject<PlayerStartInfoData>(message, ConnectionConfig.JsonSettings);
                Color skinColor = new Color(
                    playerStartInfo.playerData.skinColor.r,
                    playerStartInfo.playerData.skinColor.g, 
                    playerStartInfo.playerData.skinColor.b,
                    playerStartInfo.playerData.skinColor.a);
                _playerSprite.color = skinColor;
            }
        }
    }
}