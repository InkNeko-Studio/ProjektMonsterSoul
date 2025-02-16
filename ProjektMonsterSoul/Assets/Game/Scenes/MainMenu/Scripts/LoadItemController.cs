using Framework.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scenes.MainMenu.Scripts
{
    public class LoadItemController : MonoBehaviour
    {
        [SerializeField] private Button loadButton;
        [SerializeField] private TMP_Text nameText;
        
        public void SetIndex(int index)
        {
            if (SaveController.SaveSlots[index] != null)
            {
                nameText.text = SaveController.SaveSlots[index].playerData.name;

                loadButton.onClick.RemoveAllListeners();
                loadButton.onClick.AddListener(() =>
                {
                    SaveController.SelectSave(index);
                    SceneManager.LoadScene("GuildEntrance");
                });
            }
            else
            {
                nameText.text = "No Save";
            }
        }
    }
}