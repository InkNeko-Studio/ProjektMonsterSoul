using System;
using System.Collections.Generic;
using Framework.AreaSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class PlayerData
    {
        public string name = "Player";
        public int life = 100;
        public Weapon equippedWeapon = new Weapon() { damage = 5 };
        public List<ItemData> items = new List<ItemData>();

        public void AddItem(ItemData newItem)
        {
            var oldItem = items.Find((x) => x.item == newItem.item);
            if (oldItem != null)
                oldItem.quantity += newItem.quantity;
            else
                items.Add(newItem);
        }
    }
}