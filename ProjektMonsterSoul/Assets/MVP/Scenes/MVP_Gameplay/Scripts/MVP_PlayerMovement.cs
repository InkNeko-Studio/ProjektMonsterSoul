using System;
using Framework.Connection;
using Framework.SaveSystem;
using Game.Shared.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;

public class MVP_PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float moveSpeed;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    
    private MVP_PlayerController _playerController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<MVP_PlayerController>();
    }
    private void OnEnable() { _playerController.OnMovement += OnMovement; }
    private void OnDisable() { _playerController.OnMovement -= OnMovement; }

    private void OnMovement(Vector2 movement)
    {
        _rigidbody.linearVelocity = movement * moveSpeed;
    }
}
