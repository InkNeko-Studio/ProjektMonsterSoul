using System;
using System.Collections.Generic;
using Framework.AreaSystem;
using Framework.SaveSystem.Data;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class NetworkPlayerSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private NetworkPlayerController prefab;
        
        private List<NetworkPlayerController> _players;
        
        private void Start()
        {
            _players = new List<NetworkPlayerController>();
            if (PlayerDataManager.PlayerDataList == null) return;
            foreach (var playerData in PlayerDataManager.PlayerDataList)
                OnNewPlayer(playerData);
        }

        private void OnEnable()
        {
            PlayerDataManager.OnNewPlayer += OnNewPlayer;
            PlayerDataManager.OnUpdatePlayer += OnUpdatePlayer;
        }

        private void OnDisable()
        {
            PlayerDataManager.OnNewPlayer -= OnNewPlayer;
            PlayerDataManager.OnUpdatePlayer -= OnUpdatePlayer;
        }
        private void OnNewPlayer(PlayerData playerData)
        {
            if (playerData == null) return;
            if (playerData.area != AreaController.Instance.area) return;
            
            var newPlayer = Instantiate(prefab, transform.position, Quaternion.identity);
            newPlayer.playerData = playerData;
            newPlayer.transform.position = new Vector3(playerData.positionX, playerData.positionY, 0f);
            _players.Add(newPlayer);
        }

        private void OnUpdatePlayer(PlayerData playerData)
        {
            if (playerData == null) return;
            NetworkPlayerController player;
            if ((player = _players.Find((p) => p.playerData.name == playerData.name)) != null)
            {
                if (playerData.area != AreaController.Instance.area)
                {
                    _players.Remove(player);
                    Destroy(player.gameObject);
                }
                else
                {
                    player.playerData = playerData;
                    player.transform.position = new Vector3(playerData.positionX, playerData.positionY, 0f);
                }
            }
            else OnNewPlayer(playerData);
        }

        [ContextMenu("TestNewChest")]
        public void TestNewChest()
        {
            PlayerData playerData = new PlayerData();
            playerData.name = "ROBSON DAS COXINHAS";
            playerData.skinColor = Color.white;
            playerData.area = Area.Chest;
            
            OnNewPlayer(playerData);
        }

        [ContextMenu("TestNewBedroom")]
        public void TestNewBedroom()
        {
            PlayerData playerData = new PlayerData();
            playerData.name = "ROBSON DAS COXINHAS";
            playerData.skinColor = Color.white;
            playerData.area = Area.BedroomBed;
            
            OnNewPlayer(playerData);
        }

        [ContextMenu("TestUpdateDelete")]
        public void TestUpdateDelete()
        {
            PlayerData playerData = new PlayerData();
            playerData.name = "ROBSON DAS COXINHAS";
            playerData.skinColor = Color.white;
            playerData.area = Area.BedroomBed;
            
            OnUpdatePlayer(playerData);
        }
    }
}
