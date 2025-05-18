using System;
using System.Collections.Generic;
using Framework.AreaSystem;
using Framework.ItemSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Framework.SaveSystem.Data
{
    [Serializable]
    public class PlayerData
    {
        public string name = "Player";
        public int life = 100;
        public WeaponId equippedWeapon = WeaponId.DefaultSword;

        public List<WeaponId> weapons = new List<WeaponId>()
        {
            WeaponId.DefaultSword
        };
        public List<MaterialData> materials = new List<MaterialData>()
        {
            new MaterialData()
            {
                quantity = 9,
                materialId = MaterialId.SlimeMaterial1
            },
            new MaterialData()
            {
                quantity = 2,
                materialId = MaterialId.SlimeMaterial2
            },
            new MaterialData()
            {
                quantity = 7,
                materialId = MaterialId.SlimeMaterial3
            },
            new MaterialData()
            {
                quantity = 4,
                materialId = MaterialId.SlimeMaterial4
            },
        };

        public List<WeaponId> unlockedWeapons = new List<WeaponId>
        {
            WeaponId.DefaultSword,
            WeaponId.SlimeSword
        };

        public void AddMaterial(MaterialData newMaterial)
        {
            var oldMaterial = materials.Find((x) => x.materialId == newMaterial.materialId);
            if (oldMaterial != null)
                oldMaterial.quantity += newMaterial.quantity;
            else
                materials.Add(newMaterial);
        }

        public void RemoveMaterial(MaterialData newMaterial)
        {
            var oldMaterial = materials.Find((x) => x.materialId == newMaterial.materialId);
            if (oldMaterial == null) return;
            
            oldMaterial.quantity -= newMaterial.quantity;
            
            if (oldMaterial.quantity <= 0) materials.Remove(oldMaterial);
        }
    }
}