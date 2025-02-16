using System.Collections.Generic;
using Framework.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scenes.MainMenu.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] private CanvasGroup mainMenuScreen;
        [SerializeField] private CanvasGroup newGameScreen;
        [SerializeField] private CanvasGroup loadGameScreen;
        
        [Header("MainMenu")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button loadGameButton;
        
        [Header("NewGame")]
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private Image characterImage;
        [SerializeField] private Button createCharacterButton;
        
        [Header("LoadGame")]
        [SerializeField] private List<LoadItemController> loadItems;
        
        private void Start()
        {
            SaveController.LoadAllSaves();
            
            HideAllScreens();
            mainMenuScreen.alpha = 1f;
            mainMenuScreen.interactable = true;
            mainMenuScreen.blocksRaycasts = true;
            
            newGameButton.onClick.AddListener(() =>
            {
                HideAllScreens();
                newGameScreen.alpha = 1f;
                newGameScreen.interactable = true;
                newGameScreen.blocksRaycasts = true;
            });
            
            loadGameButton.onClick.AddListener(() =>
            {
                HideAllScreens();
                loadGameScreen.alpha = 1f;
                loadGameScreen.interactable = true;
                loadGameScreen.blocksRaycasts = true;

                for (int i = 0; i < loadItems.Count; i++)
                    loadItems[i].SetIndex(i);
            });
            
            createCharacterButton.onClick.AddListener(() =>
            {
                SaveController.NewSave();
                SaveController.CurrentSave.playerData.name = nameInput.text;
                SaveController.CurrentSave.playerData.skinColor.r = characterImage.color.r;
                SaveController.CurrentSave.playerData.skinColor.g = characterImage.color.g;
                SaveController.CurrentSave.playerData.skinColor.b = characterImage.color.b;
                SaveController.CurrentSave.playerData.skinColor.a = characterImage.color.a;
                SceneManager.LoadScene("GuildEntrance");
            });
        }

        private void HideAllScreens()
        {
            mainMenuScreen.alpha = 0f;
            mainMenuScreen.interactable = false;
            mainMenuScreen.blocksRaycasts = false;
            newGameScreen.alpha = 0f;
            newGameScreen.interactable = false;
            newGameScreen.blocksRaycasts = false;
            loadGameScreen.alpha = 0f;
            loadGameScreen.interactable = false;
            loadGameScreen.blocksRaycasts = false;
        }
    }
}
