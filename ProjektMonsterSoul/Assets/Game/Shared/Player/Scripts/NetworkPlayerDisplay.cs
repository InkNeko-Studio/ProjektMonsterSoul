using TMPro;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text playerName;

        private NetworkPlayerController _networkPlayerController;
        
        private void Start()
        {
            _networkPlayerController = GetComponent<NetworkPlayerController>();
            playerName.text = _networkPlayerController.playerData.name;
        }
    }
}