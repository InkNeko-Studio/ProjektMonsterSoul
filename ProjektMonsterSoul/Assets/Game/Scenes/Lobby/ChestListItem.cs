using System.Collections.Generic;
using Framework.ItemSystem;
using Framework.SaveSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class ChestListItem : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemQuantity;
        
        public List<ItemScriptable> items;
        
        public void SetData(ItemData data)
        {
            var foundItem = items.Find(x => x.item == data.item);
            if (foundItem == null) return;

            itemSprite.sprite = foundItem.sprite;
            itemQuantity.text = data.quantity.ToString();
        }
    }
}
