using System;
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
    }
}