using System;
using Framework.Connection;
using Framework.SaveSystem;
using Game.Shared.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    
    private PlayerController _playerController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }
    private void OnEnable() { _playerController.OnMovement += OnMovement; }
    private void OnDisable() { _playerController.OnMovement -= OnMovement; }

    private void OnMovement(Vector2 movement)
    {
        _rigidbody.linearVelocity = movement * moveSpeed;
        var data = new PlayerMovementData();
        data.playerName = SaveController.CurrentSave.playerData.name;
        data.time = Time.time;
        data.speedX = _rigidbody.linearVelocity.x;
        data.speedY = _rigidbody.linearVelocity.y;
        data.positionX = transform.position.x;
        data.positionY = transform.position.y;
        ConnectionManager.Send(JsonConvert.SerializeObject(data), ConnectionProtocol.Udp);
    }
}
