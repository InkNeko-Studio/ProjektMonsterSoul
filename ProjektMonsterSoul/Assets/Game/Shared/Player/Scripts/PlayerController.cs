using System;
using Framework.SaveSystem;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public bool blockMovement;
        public bool blockInteract;
        public bool blockAttack;
        
        public Action<Vector2> OnMovement;
        public Action OnInteract;
        public Action OnAttack;
        public Action OnDash;
        
        public Action OnUseItem;
        public Action<float> OnSelectItem;
        
        private PlayerInputActions _playerInputActions;

        private int _interactableId;
        private IInteractable _interactable;
        
        private void Awake()
        {
            Instance = this;
            _playerInputActions = new PlayerInputActions();
        }

        private void Start()
        {
            _playerInputActions.Player.Move.performed += (ctx) =>
            {
                if (blockMovement) return;
                OnMovement?.Invoke(ctx.ReadValue<Vector2>());
            };
            _playerInputActions.Player.Move.canceled += (ctx) => {
                OnMovement?.Invoke(Vector2.zero);
            };
            _playerInputActions.Player.Interactable.performed += (ctx) =>
            {
                if (blockInteract) return;
                OnInteract?.Invoke();
            };
            _playerInputActions.Player.Attack.performed += (ctx) =>
            {
                //if (blockAttack) return;
                OnAttack?.Invoke();
            };
            _playerInputActions.Player.Dash.performed += (ctx) =>
            {
                OnDash?.Invoke();
            };
            _playerInputActions.Player.UseItem.performed += (ctx) =>
            {
                OnUseItem?.Invoke();
            };
            _playerInputActions.Player.SelectItem.performed += (ctx) =>
            {
                OnSelectItem?.Invoke(ctx.ReadValue<float>());
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