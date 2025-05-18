using System;
using Framework.ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class CraftListWeapon : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemName;
        public Button selectButton;
        
        public void SetData(WeaponId weaponId, Action<WeaponId> callback)
        {
            var foundWeapon = AllWeapon.GetWeapon(weaponId);
            if (foundWeapon == null) return;

            itemSprite.sprite = foundWeapon.weaponSprite;
            itemName.text = foundWeapon.weaponName;
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() =>
            {
                callback?.Invoke(weaponId);
            });
        }
    }
}