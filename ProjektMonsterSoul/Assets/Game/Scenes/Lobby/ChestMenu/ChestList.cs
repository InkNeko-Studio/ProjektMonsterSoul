using System;
using System.Collections;
using Framework.SaveSystem;
using Game.Scenes.Lobby.ChestMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class ChestList : MonoBehaviour
    {
        [Header("Tabs")]
        public Button materialsButton;
        public Button consumablesButton;
        public Button weaponsButton;
        
        [Header("Content")]
        public RectTransform content;
        public ChestListItemMaterial materialPrefab;
        public ChestListItemWeapon weaponPrefab;
        
        private void OnEnable()
        {
            EnableButtons();
            materialsButton.interactable = false;
            ListMaterial();
            
            materialsButton.onClick.RemoveAllListeners();
            materialsButton.onClick.AddListener(() =>
            {
                EnableButtons();
                materialsButton.interactable = false;
                ListMaterial();
            });
            
            consumablesButton.onClick.RemoveAllListeners();
            consumablesButton.onClick.AddListener(() =>
            {
                EnableButtons();
                consumablesButton.interactable = false;
                ListConsumables();
            });
            
            weaponsButton.onClick.RemoveAllListeners();
            weaponsButton.onClick.AddListener(() =>
            {
                EnableButtons();
                weaponsButton.interactable = false;
                ListWeapons();
            });
        }

        private void EnableButtons()
        {
            materialsButton.interactable = true;
            consumablesButton.interactable = true;
            weaponsButton.interactable = true;
        }

        private void ListMaterial()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var material in SaveController.CurrentSave.playerData.materials)
            {
                var matInst = Instantiate(materialPrefab, content);
                matInst.SetData(material);
            }

            StartCoroutine(FixContent());
        }

        private void ListConsumables()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            StartCoroutine(FixContent());
        }

        private void ListWeapons()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            foreach (var weapon in SaveController.CurrentSave.playerData.weapons)
            {
                var weaponInst = Instantiate(weaponPrefab, content);
                weaponInst.SetData(weapon, weapon == SaveController.CurrentSave.playerData.equippedWeapon, wId =>
                {
                    SaveController.CurrentSave.playerData.equippedWeapon = wId;
                    ListWeapons();
                });
            }

            StartCoroutine(FixContent());
        }

        private IEnumerator FixContent()
        {
            yield return null;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }
    }
}
