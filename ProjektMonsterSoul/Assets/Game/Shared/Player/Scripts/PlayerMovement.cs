using System;
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
        _movement = movement * moveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _movement;
    }
}
