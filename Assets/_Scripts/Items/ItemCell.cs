using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private AspectRatioFitter imageRatio;
    [SerializeField]
    private TextMeshProUGUI textCount;
    [SerializeField]
    private TextMeshProUGUI textDiscription;
    private GameManager gameManager;

    public void Initialized(GameManager gameManager, Item item)
    {
        this.gameManager = gameManager;
        SetItem(item);
    }

    private void SetItem(Item item)
    {
        image.sprite = gameManager.GetSprite(item.ID);
        Rect imageRect = image.sprite.rect;
        imageRatio.aspectRatio = imageRect.width / imageRect.height;
        if (item.Count > 1)
        {
            textCount.enabled = true;
            textCount.text = item.Count.ToString();
        }
        else
            textCount.enabled = false;
        textDiscription.text = item.Discription;
    }
}