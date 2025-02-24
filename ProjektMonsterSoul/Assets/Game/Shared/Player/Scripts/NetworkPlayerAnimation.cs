using Framework.Connection;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerAnimation : MonoBehaviour
    {
        private NetworkPlayerController _networkPlayerController;
        private Animator _animator;
        private float _lastTime = 0f;
        
        private void Start()
        {
            _networkPlayerController = GetComponent<NetworkPlayerController>();
            _animator = GetComponentInChildren<Animator>();
            ConnectionManager.OnMessage += OnMessage;
        }

        private void OnMessage(NetworkTag tag, string message)
        {
            if (tag == NetworkTag.PlayerMovement)
            {
                var movementData = JsonConvert.DeserializeObject<PlayerMovementData>(message, ConnectionConfig.JsonSettings);
                if (movementData.playerName != _networkPlayerController.playerData.name) return;
                if (movementData.time < _lastTime) return;
                _animator?.SetFloat("SpeedX", movementData.speedX);
                _animator?.SetFloat("SpeedY", movementData.speedY);
            }
        }
    }
}