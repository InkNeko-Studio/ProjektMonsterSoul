using System.Collections.Generic;
using Framework.ItemSystem;
using Framework.SaveSystem;
using Framework.SaveSystem.Data;
using Game.Shared.Player.Scripts;
using UnityEngine;

public class CollectConsumable : MonoBehaviour, IInteractable
{
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite closedSprite;
    
    public Vector2 quantityRange;
    public List<ConsumableId> possibleDrops;
    
    public void OnInteract()
    {
        int qtd = Random.Range((int)quantityRange.x, (int)quantityRange.y);
        int rng = Random.Range(0, possibleDrops.Count);
        ConsumableId drop = possibleDrops[rng];
        SaveController.CurrentSave.playerData.AddConsumable(new ConsumableData()
        {
            quantity = qtd,
            consumableId = drop
        });
        spriteRenderer.sprite = closedSprite;
        Destroy(boxCollider);
    }

    public void OnInteractionEnter()
    {
    }

    public void OnInteractionLeave()
    {
    }
}
