using System;
using Framework.Connection;
using UnityEngine;
using Newtonsoft.Json;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _lastTime = 0f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            ConnectionManager.OnMessage += OnMessage;
        }

        private void OnMessage(NetworkTag tag, string message)
        {
            if (tag == NetworkTag.PlayerMovement)
            {
                var movementData = JsonConvert.DeserializeObject<PlayerMovementData>(message, ConnectionConfig.JsonSettings);
                if (movementData.time < _lastTime) return;
                _lastTime = movementData.time;
                _rigidbody.linearVelocity = new Vector2(movementData.speedX, movementData.speedY);
                transform.position = new Vector2(movementData.positionX, movementData.positionY);
            }
        }
    }
}