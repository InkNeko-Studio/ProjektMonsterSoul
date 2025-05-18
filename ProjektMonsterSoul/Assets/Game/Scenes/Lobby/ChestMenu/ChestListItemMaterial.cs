using Framework.ItemSystem;
using Framework.SaveSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby.ChestMenu
{
    public class ChestListItemMaterial : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemName;
        public TMP_Text itemQuantity;
        
        public void SetData(MaterialData data)
        {
            var foundMaterial = AllMaterial.GetMaterial(data.materialId);
            if (foundMaterial == null) return;

            itemSprite.sprite = foundMaterial.materialSprite;
            itemName.text = foundMaterial.materialName;
            itemQuantity.text = data.quantity.ToString();
        }
    }
}
