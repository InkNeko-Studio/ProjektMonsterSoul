using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "AllWeapon", menuName = "Items/AllWeapon")]
    public class AllWeapon : ScriptableObject
    {
        private static AllWeapon _instance;

        private static AllWeapon GetInstance()
        {
            if (_instance == null)
                _instance = Resources.Load("AllWeapon") as AllWeapon;
            return _instance;
        }

        public List<WeaponScriptable> weapons = new List<WeaponScriptable>();

        public static WeaponScriptable GetWeapon(WeaponId id)
        {
            return GetInstance().weapons.Find(m => m.weaponId == id);
        }
    }
}