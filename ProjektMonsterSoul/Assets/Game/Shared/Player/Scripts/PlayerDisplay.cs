using Framework.SaveSystem;
using TMPro;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text playerName;

        private void Start()
        {
            playerName.text = SaveController.CurrentSave.playerData.name;
        }
    }
}