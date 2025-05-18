using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.ItemSystem
{
    [Serializable]
    public class CraftData
    {
        public int quantity;
        public MaterialId material;
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class WeaponScriptable : ScriptableObject
    {
        public WeaponId weaponId;
        public string weaponName;
        public Sprite weaponSprite;
        public int weaponDamage;
        public List<CraftData> craftData;
    }
}