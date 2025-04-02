using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MVP_LobbyController : MonoBehaviour
{
    public Button play;
    void Start()
    {
        play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MVP_Gameplay1");
        });
    }
}
