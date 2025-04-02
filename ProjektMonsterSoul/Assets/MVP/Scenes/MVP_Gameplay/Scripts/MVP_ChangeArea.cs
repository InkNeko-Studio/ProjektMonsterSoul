using UnityEngine;
using UnityEngine.SceneManagement;

public class MVP_ChangeArea : MonoBehaviour
{
    public string areaName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(areaName);
    }
}
