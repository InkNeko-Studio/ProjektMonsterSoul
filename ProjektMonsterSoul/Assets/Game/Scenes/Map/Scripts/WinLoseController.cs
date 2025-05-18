using System;
using System.Collections;
using System.Collections.Generic;
using Framework.ItemSystem;
using Framework.SaveSystem;
using Framework.SaveSystem.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.Scenes.Map.Scripts
{
    [Serializable]
    public class DropData
    {
        public Vector2Int dropRange;
        public MaterialId materialId;
    }
    
    public class WinLoseController : MonoBehaviour
    {
        public static WinLoseController Instance;

        public GameObject player;
        public GameObject enemy;
        
        public GameObject youWin;
        public GameObject youLose;

        public List<Image> winImages;
        public List<DropData> dropMaterials;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Win()
        {
            StartCoroutine(WinLoseCoroutine(true));
            enemy.SetActive(false);
            
            foreach (var winImage in winImages)
            {
                int id = Random.Range(0, dropMaterials.Count);
                var foundMaterial = AllMaterial.GetMaterial(dropMaterials[id].materialId);
                winImage.sprite = foundMaterial.materialSprite;
                SaveController.CurrentSave.playerData.AddMaterial(new MaterialData()
                {
                    materialId = foundMaterial.materialId,
                    quantity = Random.Range(dropMaterials[id].dropRange.x, dropMaterials[id].dropRange.y)
                });
            }
        }
        
        public void Lose()
        {
            StartCoroutine(WinLoseCoroutine(false));
            player.SetActive(false);
        }

        private IEnumerator WinLoseCoroutine(bool win)
        {
            if (win) youWin.SetActive(true);
            else youLose.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene("Lobby");
        }
    }
}
