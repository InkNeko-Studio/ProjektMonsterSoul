using UnityEngine;

namespace Framework.ItemSystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class ItemScriptable : ScriptableObject
    {
        public Item item;
        public Sprite sprite;
    }
}