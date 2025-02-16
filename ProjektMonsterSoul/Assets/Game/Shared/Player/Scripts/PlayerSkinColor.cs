using System;
using System.Collections;
using Framework.Connection;
using Framework.SaveSystem;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerSkinColor : MonoBehaviour
    {
        private SpriteRenderer _playerSprite;

        private void Start()
        {
            _playerSprite = GetComponentInChildren<SpriteRenderer>();
            Color skinColor = new Color(
                SaveController.CurrentSave.playerData.skinColor.r,
                SaveController.CurrentSave.playerData.skinColor.g, 
                SaveController.CurrentSave.playerData.skinColor.b,
                SaveController.CurrentSave.playerData.skinColor.a);
            _playerSprite.color = skinColor;
        }
    }
}