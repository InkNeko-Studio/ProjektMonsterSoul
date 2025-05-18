using System;
using Framework.ItemSystem;
using Framework.SaveSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby.ChestMenu
{
    public class ChestListItemWeapon : MonoBehaviour
    {
        public Image itemSprite;
        public TMP_Text itemName;
        public Button equipButton;
        
        public void SetData(WeaponId weaponId, bool equipped, Action<WeaponId> onEquip)
        {
            var foundWeapon = AllWeapon.GetWeapon(weaponId);
            if (foundWeapon == null) return;

            itemSprite.sprite = foundWeapon.weaponSprite;
            itemName.text = foundWeapon.weaponName;
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(() =>
            {
                onEquip?.Invoke(weaponId);
            });
            equipButton.interactable = !equipped;
        }
    }
}
