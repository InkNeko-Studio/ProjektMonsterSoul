using Game.Scenes.Connect.Scripts;
using Game.Shared.Player.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scenes.GuildEntrance.Scripts
{
    public class GuildDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private CanvasGroup interactionIcon;

        private void Start()
        {
            interactionIcon.alpha = 0f;
        }

        public void OnInteract()
        {
            ConnectController.Show(new ConnectControllerData()
            {
                OnLobby = () =>
                {
                    SceneManager.LoadScene("Chest");
                },
                OnHost = PlayerDataManager.Start,
                OnConnect = () =>
                {
                    PlayerDataManager.Start();
                    PlayerDataManager.SendData();
                }
            });
        }

        public void OnInteractionEnter()
        {
            interactionIcon.alpha = 1f;
        }

        public void OnInteractionLeave()
        {
            interactionIcon.alpha = 0f;
        }
    }
}
