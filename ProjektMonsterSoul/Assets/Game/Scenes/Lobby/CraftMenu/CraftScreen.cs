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
        public Button swordsButton;

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
            swordsButton.interactable = false;
            ListSwords();
            
            swordsButton.onClick.RemoveAllListeners();
            swordsButton.onClick.AddListener(() =>
            {
                EnableButtons();
                swordsButton.interactable = false;
                ListSwords();
            });
            
            craftButton.onClick.RemoveAllListeners();
            craftButton.onClick.AddListener(Craft);
        }

        private void EnableButtons()
        {
            swordsButton.interactable = true;
        }

        private void ListSwords()
        {
            weaponInfo.SetActive(false);
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var unlockedWeapon in SaveController.CurrentSave.playerData.unlockedWeapons)
            {
                if (!SaveController.CurrentSave.playerData.weapons.Contains(unlockedWeapon))
                {
                    var weaponInst = Instantiate(weaponPrefab, content);
                    weaponInst.SetData(unlockedWeapon, OnWeaponClick);
                }
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
                    Debug.LogError("Ativou");
                }
                else
                {
                    craftMaterial.gameObject.SetActive(false);
                    Debug.LogError("Desativou");
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
            SaveController.CurrentSave.playerData.weapons.Add(_selectedWeapon);
            ListSwords();
        }
        
        private IEnumerator FixContent()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }
    }
}
