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
            _playerSprite.color = SaveController.CurrentSave.playerData.skinColor;
        }
    }
}