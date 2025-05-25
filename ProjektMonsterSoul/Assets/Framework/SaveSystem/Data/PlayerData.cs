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
        public int maxLife = 100;
        public WeaponId equippedWeapon = WeaponId.DefaultSword;
        
        public List<WeaponId> weapons = new List<WeaponId>()
        {
            WeaponId.DefaultSword,
            WeaponId.DefaultGreatSword,
            WeaponId.DefaultLance,
            WeaponId.DefaultDagger
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
        public List<ConsumableData> consumables = new List<ConsumableData>()
        {
            new ConsumableData()
            {
                quantity = 10,
                consumableId = ConsumableId.RedPotion,
            },
            new ConsumableData()
            {
                quantity = 10,
                consumableId = ConsumableId.GreenPotion,
            },
        };

        public List<WeaponId> unlockedWeapons = new List<WeaponId>
        {
            WeaponId.DefaultSword,
            WeaponId.DefaultGreatSword,
            WeaponId.DefaultLance,
            WeaponId.DefaultDagger
        };

        public void AddMaterial(MaterialData newMaterial)
        {
            var oldMaterial = materials.Find((x) => x.materialId == newMaterial.materialId);
            if (oldMaterial != null)
                oldMaterial.quantity += newMaterial.quantity;
            else
                materials.Add(newMaterial);
            
            materials.Sort((a, b) => a.materialId.CompareTo(b.materialId));
        }

        public void RemoveMaterial(MaterialData newMaterial)
        {
            var oldMaterial = materials.Find((x) => x.materialId == newMaterial.materialId);
            if (oldMaterial == null) return;
            
            oldMaterial.quantity -= newMaterial.quantity;
            
            if (oldMaterial.quantity <= 0) materials.Remove(oldMaterial);
            
            materials.Sort((a, b) => a.materialId.CompareTo(b.materialId));
        }

        public Action OnConsumableChanged;
        
        public void AddConsumable(ConsumableData newConsumable)
        {
            var oldConsumable = consumables.Find((x) => x.consumableId == newConsumable.consumableId);
            if (oldConsumable != null)
                oldConsumable.quantity += newConsumable.quantity;
            else
                consumables.Add(newConsumable);
            
            consumables.Sort((a, b) => a.consumableId.CompareTo(b.consumableId));
            OnConsumableChanged?.Invoke();
        }

        public void RemoveConsumable(ConsumableData newConsumable)
        {
            var oldConsumable = consumables.Find((x) => x.consumableId == newConsumable.consumableId);
            if (oldConsumable == null) return;
            
            oldConsumable.quantity -= newConsumable.quantity;
            
            if (oldConsumable.quantity <= 0) consumables.Remove(oldConsumable);
            
            consumables.Sort((a, b) => a.consumableId.CompareTo(b.consumableId));
            OnConsumableChanged?.Invoke();
        }

        public void AddWeapon(WeaponId newWeapon)
        {
            if (weapons.Contains(newWeapon)) return;
            weapons.Add(newWeapon);
            weapons.Sort();
        }

        public void UnlockWeapon(WeaponId newWeapon)
        {
            if (unlockedWeapons.Contains(newWeapon)) return;
            unlockedWeapons.Add(newWeapon);
            weapons.Sort();
        }
    }
}