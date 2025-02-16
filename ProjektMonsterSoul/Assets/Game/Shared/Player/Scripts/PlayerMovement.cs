using System;
using Framework.SaveSystem;
using Game.Shared.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text playerName;
    
    [SerializeField] private float movespeed;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private Animator _animator;
    
    private PlayerController _playercontroller;

    private int _interactableId;
    private IInteractable _interactable;
    
    private void Awake()
    {
        _playercontroller = new PlayerController();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        
        // Configure Input
        _playercontroller.Player.Move.performed += (ctx) => {
            _movement = ctx.ReadValue<Vector2>();
        };
        _playercontroller.Player.Move.canceled += (ctx) => {
            _movement = Vector2.zero;
        };
        _playercontroller.Player.Interactable.performed += (ctx) => {
            _interactable?.OnInteract();
        };

        playerName.text = SaveController.CurrentSave.playerData.name;
    }

    private void OnEnable()
    {
        _playercontroller.Enable();
    }

    private void OnDisable()
    {
        _playercontroller.Disable();
    }

    private void Update()
    {
        _rigidbody.linearVelocity = _movement * movespeed;
        Animations();
    }

    private void Animations()
    {
        _animator.SetFloat("X", _movement.x);
        _animator.SetFloat("Y", _movement.y);
        _animator.SetBool("Idle", _movement.x == 0 && _movement.y == 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactableId = other.GetInstanceID();
            _interactable = other.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetInstanceID() == _interactableId)
        {
            _interactable = null;
        }
    }
}
