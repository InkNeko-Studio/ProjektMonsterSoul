using System;
using Framework.ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class CraftListMaterial : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemQuantity;
        
        public void SetData(MaterialId material, int quantity)
        {
            var mat = AllMaterial.GetMaterial(material);
            itemSprite.sprite = mat.materialSprite;
            itemQuantity.text = quantity.ToString();
        }
    }
}