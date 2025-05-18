using Framework.ItemSystem;
using Framework.SaveSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby.ChestMenu
{
    public class ChestListItemConsumable : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemName;
        public TMP_Text itemQuantity;
        
        public void SetData(ConsumableData data)
        {
            var foundConsumable = AllConsumable.GetConsumable(data.consumableId);
            if (foundConsumable == null) return;

            itemSprite.sprite = foundConsumable.consumableSprite;
            itemName.text = foundConsumable.consumableName;
            itemQuantity.text = data.quantity.ToString();
        }
    }
}
