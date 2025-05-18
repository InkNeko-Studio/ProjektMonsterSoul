using System;
using System.Collections;
using System.Collections.Generic;
using Framework.ItemSystem;
using Framework.SaveSystem;
using Framework.SaveSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class CraftScreen : MonoBehaviour
    {
        [Header("Tabs")]
        public Button swordButton;
        public Button greatSwordButton;
        public Button lanceButton;
        public Button daggerButton;

        [Header("Content")]
        public RectTransform content;
        public CraftListWeapon weaponPrefab;

        [Header("WeaponInfo")]
        public GameObject weaponInfo;
        public List<CraftListMaterial> craftListMaterials;
        public Button craftButton;

        private WeaponId _selectedWeapon;
        
        private void OnEnable()
        {
            EnableButtons();
            swordButton.interactable = false;
            ListWeapons(WeaponClassId.Sword);
            
            swordButton.onClick.RemoveAllListeners();
            swordButton.onClick.AddListener(() =>
            {
                EnableButtons();
                swordButton.interactable = false;
                ListWeapons(WeaponClassId.Sword);
            });
            
            greatSwordButton.onClick.RemoveAllListeners();
            greatSwordButton.onClick.AddListener(() =>
            {
                EnableButtons();
                greatSwordButton.interactable = false;
                ListWeapons(WeaponClassId.GreatSword);
            });
            
            lanceButton.onClick.RemoveAllListeners();
            lanceButton.onClick.AddListener(() =>
            {
                EnableButtons();
                lanceButton.interactable = false;
                ListWeapons(WeaponClassId.Lance);
            });
            
            daggerButton.onClick.RemoveAllListeners();
            daggerButton.onClick.AddListener(() =>
            {
                EnableButtons();
                daggerButton.interactable = false;
                ListWeapons(WeaponClassId.Dagger);
            });
            
            craftButton.onClick.RemoveAllListeners();
            craftButton.onClick.AddListener(Craft);
        }

        private void EnableButtons()
        {
            swordButton.interactable = true;
            greatSwordButton.interactable = true;
            lanceButton.interactable = true;
            daggerButton.interactable = true;
        }

        private void ListWeapons(WeaponClassId classId)
        {
            weaponInfo.SetActive(false);
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var unlockedWeapon in SaveController.CurrentSave.playerData.unlockedWeapons)
            {
                if (SaveController.CurrentSave.playerData.weapons.Contains(unlockedWeapon)) continue;
                if (WeaponIdHelper.GetClass(unlockedWeapon) != classId) continue;
                var weaponInst = Instantiate(weaponPrefab, content);
                weaponInst.SetData(unlockedWeapon, OnWeaponClick);
            }

            StartCoroutine(FixContent());
        }

        private void OnWeaponClick(WeaponId weaponId)
        {
            _selectedWeapon = weaponId;
            weaponInfo.SetActive(true);
            var weapon = AllWeapon.GetWeapon(weaponId);
            int i = 0;
            foreach (var craftMaterial in craftListMaterials)
            {
                if (i < weapon.craftData.Count)
                {
                    craftMaterial.gameObject.SetActive(true);
                    craftMaterial.SetData(weapon.craftData[i].material, weapon.craftData[i].quantity);
                }
                else
                {
                    craftMaterial.gameObject.SetActive(false);
                }

                i++;
            }

            bool canCraft = true;
            foreach (var craftData in weapon.craftData)
            {
                var found = SaveController.CurrentSave.playerData.materials.Find(m => m.materialId == craftData.material);
                if (found == null)
                {
                    canCraft = false;
                    break;
                }

                if (found.quantity < craftData.quantity)
                {
                    canCraft = false;
                    break;
                }
            }
            
            craftButton.interactable = canCraft;
        }

        private void Craft()
        {
            var weapon = AllWeapon.GetWeapon(_selectedWeapon);
            foreach (var craftData in weapon.craftData)
            {
                SaveController.CurrentSave.playerData.RemoveMaterial(new MaterialData()
                {
                    quantity = craftData.quantity,
                    materialId = craftData.material
                });
            }
            SaveController.CurrentSave.playerData.AddWeapon(_selectedWeapon);
            ListWeapons(WeaponIdHelper.GetClass(_selectedWeapon));
        }
        
        private IEnumerator FixContent()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }
    }
}
