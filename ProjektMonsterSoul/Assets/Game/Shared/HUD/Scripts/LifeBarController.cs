using System;
using Framework.ItemSystem;
using Framework.SaveSystem;
using Framework.SaveSystem.Data;
using Game.Shared.Player.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class LifeBarController : MonoBehaviour
{
    public PlayerController playerController;
    
    [Header("HUD Items")] 
    [SerializeField] private Image _HealthBarFill;
    [SerializeField] private Image selectItemImage;
    [SerializeField] private TMP_Text selectItemQuantity;
    
    private int _selectedItem = 0;
    private float _selectDelay = 0;

    private void Start()
    {
        playerController.OnUseItem = () =>
        {
            if (_selectedItem >= SaveController.CurrentSave.playerData.consumables.Count)
            {
                _selectedItem = 0;
                selectItemImage.sprite = null;
                selectItemImage.gameObject.SetActive(false);
                selectItemQuantity.text = "";
                return;
            }
            var consumable = SaveController.CurrentSave.playerData.consumables[_selectedItem];
            SaveController.CurrentSave.playerData.RemoveConsumable(new ConsumableData()
            {
                quantity = 1,
                consumableId = consumable.consumableId,
            });
            ConsumableUsage.Use(consumable.consumableId);
            
            ShowSelectItem();
        };
        playerController.OnSelectItem = (y) =>
        {
            _selectDelay += y;
            if (Mathf.Abs(_selectDelay) > 4f)
            {
                _selectDelay = 0;
                _selectedItem += Mathf.RoundToInt(y);
                if (_selectedItem >= SaveController.CurrentSave.playerData.consumables.Count) _selectedItem = 0;
                if (_selectedItem < 0) _selectedItem = SaveController.CurrentSave.playerData.consumables.Count - 1; 
            }
            ShowSelectItem();
        };

        ShowSelectItem();

        SaveController.CurrentSave.playerData.OnConsumableChanged = ShowSelectItem;
    }

    private void ShowSelectItem()
    {
        if (_selectedItem >= SaveController.CurrentSave.playerData.consumables.Count)
        {
            _selectedItem = 0;
            selectItemImage.sprite = null;
            selectItemImage.gameObject.SetActive(false);
            selectItemQuantity.text = "";
        }
        else
        {
            var consumable = SaveController.CurrentSave.playerData.consumables[_selectedItem];
            selectItemImage.sprite = AllConsumable.GetConsumable(consumable.consumableId).consumableSprite;
            selectItemImage.gameObject.SetActive(true);
            selectItemQuantity.text = consumable.quantity.ToString();
        }
    }

    void Update()
    {
        ChanceHealth();
    }

    private void ChanceHealth()
    {
       _HealthBarFill.fillAmount = SaveController.CurrentSave.playerData.life / 100f;
    }
}
