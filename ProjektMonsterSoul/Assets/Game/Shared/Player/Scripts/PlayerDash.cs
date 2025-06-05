using System;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    
public class PlayerDash : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private float _DashForce;
    private bool _DashCD;
    private float dashTime;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_DashCD)
        {
            dashTime += Time.deltaTime;
        }

        if (dashTime >= 1)
        {
            _DashCD = false;
            dashTime = 0;
        }

    }

    private void OnEnable() { _playerController.OnDash += OnDash; }
    private void OnDisable() { _playerController.OnDash -= OnDash; }

    public void OnDash()
    {
        if (_DashCD == false)
        {
            switch (_playerMovement.direction)
            {
                
                case Direction.Up:
                    _rigidbody.AddForce(transform.up * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    _DashCD = true;
                    animator.SetTrigger("JumpUp");
                    break;
                case Direction.Down:
                    _rigidbody.AddForce(-transform.up * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    _DashCD = true;
                    animator.SetTrigger("JumpDown");
                    break;
                case Direction.Left:
                    _rigidbody.AddForce(-transform.right * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    _DashCD = true;
                    animator.SetTrigger("JumpLeft");
                    break;
                case Direction.Right:
                    _rigidbody.AddForce(transform.right * _DashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    _DashCD = true;
                    animator.SetTrigger("JumpRight");
                    break;
            }
        }
        
    }
}
}
