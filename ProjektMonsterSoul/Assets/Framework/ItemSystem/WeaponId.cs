using System;
using UnityEngine;

namespace Framework.ItemSystem
{
    [Serializable]
    public enum WeaponId
    {
        DefaultSword = 0100,
        SlimeSword = 0101,
        BlockSword = 0102,
        
        DefaultGreatSword = 0200,
        SlimeGreatSword = 0201,
        BlockGreatSword = 0202,
        
        DefaultLance = 0300,
        SlimeLance = 0301,
        BlockLance = 0302,
        
        DefaultDagger = 0400,
        SlimeDagger = 0401,
        BlockDagger = 0402,
    }

    public enum WeaponClassId
    {
        Sword = 1,
        GreatSword = 2,
        Lance = 3,
        Dagger = 4,
    }
    
    public static class WeaponIdHelper
    {
        public static WeaponClassId GetClass(WeaponId id)
        {
            int index = (int)id;
            int classIndex = Mathf.FloorToInt(index / 100f);
            return (WeaponClassId)classIndex;
        }
    }
}