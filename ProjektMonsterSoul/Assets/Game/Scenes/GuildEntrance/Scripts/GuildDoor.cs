using System;
using Game.Scenes.Connect.Scripts;
using Game.Shared.Player.Scripts;
using UnityEngine;

public class GuildDoor : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        ConnectController.Show(new ConnectControllerData());
    }
}
