using System;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    
public class PlayerDash : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rigidbody;
    private Vector2 _OnMoviment;
    [SerializeField] private float _DashForce;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() { _playerController.OnDash += OnDash; }
    private void OnDisable() { _playerController.OnDash -= OnDash; }

    public void OnDash(Vector2 moviment)
    {
        _OnMoviment = moviment;
        switch (_playerMovement.direction)
        {
            case Direction.Up:
                _rigidbody.AddForce(_OnMoviment * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                
                break;
            case Direction.Down:
                
                break;
            case Direction.Left:
                //_rigidbody.AddForce( * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                break;
            case Direction.Right:
                
                break;
        }
    }
}
}
