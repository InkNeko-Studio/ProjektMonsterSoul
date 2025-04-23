using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scenes.Lobby.Scripts
{
    [RequireComponent(typeof(Button))]
    public class OpenLevelButton : MonoBehaviour
    {
        public string levelName;
        
        private void Awake()
        {
            var button = GetComponent<Button>();
            
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(levelName);
            });
        }
    }
}
