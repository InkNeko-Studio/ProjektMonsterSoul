using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerInteraction : MonoBehaviour
    {
        private PlayerController _playerController;
        
        private int _interactableId;
        private IInteractable _interactable;

        private void Awake() { _playerController = GetComponent<PlayerController>(); }
        private void OnEnable() { _playerController.OnInteract += OnInteract; }
        private void OnDisable() { _playerController.OnInteract -= OnInteract; }

        private void OnInteract()
        {
            _interactable?.OnInteract();
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
}