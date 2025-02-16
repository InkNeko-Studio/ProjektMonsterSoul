using Framework.SaveSystem;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerSkinColor : MonoBehaviour
    {[Header("References")]
        [SerializeField]private SpriteRenderer playersprite;

        private void Start()
        {
            playersprite.color = SaveController.CurrentSave.playerData.skinColor;
        }
    }
}