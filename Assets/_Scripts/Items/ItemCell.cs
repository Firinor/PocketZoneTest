using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : Button
{
    private InventoryCellHolder holder;
    private Item item;
    private Player player;
    private GameManager gameManager;

    public void Initialized(GameManager gameManager, Player player, Item item)
    {
        holder = GetComponent<InventoryCellHolder>();
        this.gameManager = gameManager;
        this.player = player;
        SetItem(item);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        holder.deleteButton.SetActive(false);
        base.OnDeselect(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        holder.deleteButton.SetActive(true);
        base.OnSelect(eventData);
    }

    private void SetItem(Item item)
    {
        this.item = item;
        holder.itemImage.sprite = gameManager.GetSprite(item.ID);
        Rect imageRect = image.sprite.rect;
        holder.imageRatio.aspectRatio = imageRect.width / imageRect.height;
        if (item.Count > 1)
        {
            holder.textCount.enabled = true;
            holder.textCount.text = item.Count.ToString();
        }
        else
            holder.textCount.enabled = false;
        holder.textDiscription.text = item.Discription;
    }

    public void DeleteItem()
    {
        player.DeleteItem(item);
        Destroy(gameObject);
    }
}