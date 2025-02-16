using System;
using System.Collections;
using Framework.Connection;
using Framework.SaveSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerStartInfo : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SendCoroutine());
        }

        private IEnumerator SendCoroutine()
        {
            var wait = new WaitForSeconds(1.0f);

            while (true)
            {
                PlayerStartInfoData playerStartInfo = new PlayerStartInfoData();
                playerStartInfo.playerData = SaveController.CurrentSave.playerData;
                ConnectionManager.Send(JsonConvert.SerializeObject(playerStartInfo));
                yield return wait;
            }
        }
    }
}