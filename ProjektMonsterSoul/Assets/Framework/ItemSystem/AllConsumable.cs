using System.Collections.Generic;
using UnityEngine;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "AllConsumable", menuName = "Items/AllConsumable")]
    public class AllConsumable : ScriptableObject
    {
        private static AllConsumable _instance;

        private static AllConsumable GetInstance()
        {
            if (_instance == null)
                _instance = Resources.Load("AllConsumable") as AllConsumable;
            return _instance;
        }

        public List<ConsumableScriptable> consumables = new List<ConsumableScriptable>();

        public static ConsumableScriptable GetConsumable(ConsumableId id)
        {
            return GetInstance().consumables.Find(c=> c.consumableId == id);
        }
    }
}