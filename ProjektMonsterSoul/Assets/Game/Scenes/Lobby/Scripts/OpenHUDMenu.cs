using System;
using Game.Shared.Player.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class OpenHUDMenu : MonoBehaviour
{
    [Header("HUD")] 
    [SerializeField] private GameObject _HUD;
    [SerializeField] private PlayerController _playerController;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _playerController = col.GetComponent<PlayerController>();
            _playerController.OnInteract += EnableHUD;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _playerController.OnInteract = null;
            _playerController = null;
            
        }
    }


    private void EnableHUD()
    {
        _HUD.SetActive(!_HUD.activeSelf);
    }
}
