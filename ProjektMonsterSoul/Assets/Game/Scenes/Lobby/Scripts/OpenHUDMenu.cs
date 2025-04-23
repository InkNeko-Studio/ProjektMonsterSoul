using System;
using Game.Shared.Player.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class OpenHUDMenu : MonoBehaviour, IInteractable
{
    [Header("HUD")] 
    [SerializeField] private GameObject _HUD;
    
    public void OnInteract()
    {
        _HUD.SetActive(true);
        PlayerController.Instance.blockMovement = true;
        PlayerController.Instance.blockInteract = true;
        PlayerController.Instance.blockInteract = true;
    }

    public void OnInteractionEnter()
    {
    }

    public void OnInteractionLeave()
    {
    }
}
