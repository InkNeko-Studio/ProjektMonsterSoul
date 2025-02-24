using System;
using Framework.SaveSystem.Data;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private NetworkPlayerController prefab;
        
        private void Start()
        {
            if (PlayerDataManager.PlayerDataList == null) return;
            foreach (var playerData in PlayerDataManager.PlayerDataList)
                OnNewPlayer(playerData);
        }
        private void OnEnable() { PlayerDataManager.OnNewPlayer += OnNewPlayer; }
        private void OnDisable() { PlayerDataManager.OnNewPlayer += OnNewPlayer; }
        private void OnNewPlayer(PlayerData playerData)
        {
            var newPlayer = Instantiate(prefab, transform.position, Quaternion.identity);
            newPlayer.playerData = playerData;
        }

        [ContextMenu("Test")]
        public void Test()
        {
            PlayerData playerData = new PlayerData();
            playerData.name = "ROBSON DAS COXINHAS";
            playerData.skinColor = Color.white;
            
            OnNewPlayer(playerData);
        }
    }
}
