using System;
using System.Collections.Generic;
using Framework.SaveSystem;
using Game.Shared.Player.Scripts;
using UnityEngine;

namespace Framework.AreaSystem
{
    public class AreaController : MonoBehaviour
    {
        public static AreaController Instance;
        public Area area;
        public List<AreaBorder> areaBorders;
        public Transform player;

        private void Awake()
        {
            Instance = this;
            foreach (AreaBorder areaBorder in areaBorders)
            {
                if (SaveController.CurrentSave.playerData.area == areaBorder.area)
                {
                    areaBorder.SetPlayerPosition(player);
                    break;
                }
            }
            SaveController.CurrentSave.playerData.area = area;
            SaveController.CurrentSave.playerData.positionX = player.position.x;
            SaveController.CurrentSave.playerData.positionY = player.position.y;
            PlayerDataManager.SendData();
        }
    }
}