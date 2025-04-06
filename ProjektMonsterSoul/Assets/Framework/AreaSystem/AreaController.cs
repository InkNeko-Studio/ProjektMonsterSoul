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

        private static Area _currentArea;

        private void Awake()
        {
            Instance = this;
            foreach (AreaBorder areaBorder in areaBorders)
            {
                if (_currentArea == areaBorder.area)
                {
                    areaBorder.SetPlayerPosition(player);
                    break;
                }
            }

            _currentArea = area;
        }
    }
}