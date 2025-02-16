using Framework.Connection;
using Framework.SaveSystem;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text playerName;

        private void Start()
        {
            ConnectionManager.OnMessage += OnMessage;
        }

        private void OnMessage(NetworkTag tag, string message)
        {
            if (tag == NetworkTag.PlayerStartInfo)
            {
                var playerStartInfo = JsonConvert.DeserializeObject<PlayerStartInfoData>(message, ConnectionConfig.JsonSettings);
                playerName.text = playerStartInfo.playerData.name;
            }
        }
    }
}