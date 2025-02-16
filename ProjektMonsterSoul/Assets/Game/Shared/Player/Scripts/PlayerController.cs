using System;
using Framework.SaveSystem;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public Action<Vector2> OnMovement;
        public Action OnInteract;
        
        private PlayerInputActions _playerInputActions;

        private int _interactableId;
        private IInteractable _interactable;
        
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        private void Start()
        {
            _playerInputActions.Player.Move.performed += (ctx) => {
                OnMovement?.Invoke(ctx.ReadValue<Vector2>());
            };
            _playerInputActions.Player.Move.canceled += (ctx) => {
                OnMovement?.Invoke(Vector2.zero);
            };
            _playerInputActions.Player.Interactable.performed += (ctx) => {
                OnInteract?.Invoke();
            };
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }
    }
}