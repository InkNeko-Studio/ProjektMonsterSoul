using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MVP_MainMenuController : MonoBehaviour
{
    public Button play;
    void Start()
    {
        play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MVP_Lobby");
        });
    }
}
