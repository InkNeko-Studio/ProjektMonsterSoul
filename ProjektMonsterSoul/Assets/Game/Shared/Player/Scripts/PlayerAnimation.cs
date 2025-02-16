using System;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        private PlayerController _playerController;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _playerController = GetComponent<PlayerController>();
        }
        private void OnEnable() { _playerController.OnMovement += OnMovement; }
        private void OnDisable() { _playerController.OnMovement -= OnMovement; }

        private void OnMovement(Vector2 movement)
        {
            _animator?.SetFloat("SpeedX", movement.x);
            _animator?.SetFloat("SpeedY", movement.y);
        }
    }
}