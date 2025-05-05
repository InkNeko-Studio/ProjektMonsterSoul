using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scenes.Lobby
{
    public class CraftScreen : MonoBehaviour
    {
        public GameObject weaponChoose;
        public GameObject swordChoose;
        public Button swordButton;
        public Button slimeSwordButton;

        private void Awake()
        {
            swordButton.onClick.AddListener(() =>
            {
                weaponChoose.SetActive(false);
                swordChoose.SetActive(true);
            });
            
            slimeSwordButton.onClick.AddListener(() =>
            {
                slimeSwordButton.interactable = false;
            });
        }

        private void OnEnable()
        {
            weaponChoose.SetActive(true);
            swordChoose.SetActive(false);
        }
    }
}
