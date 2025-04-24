using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Game.Scenes.Map.Scripts
{
    public class WinLoseController : MonoBehaviour
    {
        public static WinLoseController Instance;

        public GameObject player;
        public GameObject enemy;
        
        public GameObject youWin;
        public GameObject youLose;

        public List<Image> winImages;
        public List<Sprite> items;
        
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
                winImage.sprite = items[Random.Range(0, items.Count)];
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
