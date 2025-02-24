using System;
using Framework.Connection;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerSkinColor : MonoBehaviour
    {
        private NetworkPlayerController _networkPlayerController;
        private SpriteRenderer _playerSprite;
        
        private void Start()
        {
            _networkPlayerController = GetComponent<NetworkPlayerController>();
            _playerSprite = GetComponentInChildren<SpriteRenderer>();
            _playerSprite.color = _networkPlayerController.playerData.skinColor;
        }
    }
}