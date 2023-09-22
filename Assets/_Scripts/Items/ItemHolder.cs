using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private float pickUpRadius;
    [SerializeField]
    private float speedToPlayer;
    [SerializeField]
    private Image image;

    public Vector2 RectPosition => rectTransform.anchoredPosition;
    private RectTransform rectTransform;
    private ItemScriptable item;

    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(ToPlayer(collision.GetComponent<Player>()));
    }

    private IEnumerator ToPlayer(Player target)
    {
        while(Vector2.Distance(RectPosition, target.RectPosition) > pickUpRadius)
        {
            rectTransform.anchoredPosition = rectTransform.anchoredPosition + ToTarget(target) * speedToPlayer * Time.deltaTime;
            yield return null;
        }

        target.AddItem(item);
        Destroy(gameObject);
    }

    private Vector2 ToTarget(Player target)
    {
        return (target.RectPosition - RectPosition).normalized;
    }

    public void SetItem(ItemScriptable item)
    {
        this.item = item;
        image.sprite = item.Image;
        image.SetNativeSize();
    }
}
