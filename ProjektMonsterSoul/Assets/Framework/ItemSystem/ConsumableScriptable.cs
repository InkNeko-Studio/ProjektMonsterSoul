using UnityEngine;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable")]
    public class ConsumableScriptable : ScriptableObject
    {
        public ConsumableId consumableId;
        public string consumableName;
        public Sprite consumableSprite;
    }
}