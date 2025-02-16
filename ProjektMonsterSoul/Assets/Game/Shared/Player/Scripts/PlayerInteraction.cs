using UnityEngine;
using UnityEngine.UI;

namespace Game.Shared.Player.Scripts
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject interactionButton;
        
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
                if (_interactable != null) _interactable.OnInteractionLeave();
                _interactableId = other.GetInstanceID();
                _interactable = other.GetComponent<IInteractable>();
                _interactable.OnInteractionEnter();
                interactionButton.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetInstanceID() == _interactableId)
            {
                _interactable.OnInteractionLeave();
                _interactable = null;
                interactionButton.SetActive(false);
            }
        }
    }
}