using Game.Shared.Player.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby.Scripts
{
    [RequireComponent(typeof(Button))]
    public class CloseHUDMenu : MonoBehaviour
    {
        [Header("HUD")] 
        [SerializeField] private GameObject _HUD;
        
        private void Awake()
        {
            var button = GetComponent<Button>();
            
            button.onClick.AddListener(() =>
            {
                _HUD.SetActive(false);
                PlayerController.Instance.blockMovement = false;
                PlayerController.Instance.blockInteract = false;
                PlayerController.Instance.blockInteract = false;
            });
        }
    }
}
