using UnityEngine;
using Zenject;

public class InventoryObserver : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject inventoryCellPrefab;
    [Inject]
    private GameManager gameManager;
    
    public void OnInventoryBottonClick()
    {
        if (inventory.activeSelf)
            inventory.SetActive(false);
        else
        {
            inventory.SetActive(true);
            RefreshInventoryUI();
        }
    }
    private void RefreshInventoryUI()
    {
        DeleteAllChild();
        CreateItems();
    }

    private void CreateItems()
    {
        foreach(Item item in player.Inventory.Items)
        {
            var itemCellGameObject = Instantiate(inventoryCellPrefab, parentTransform);
            ItemCell cell = itemCellGameObject.GetComponent<ItemCell>();
            cell.Initialized(gameManager, player, item);
        }
    }

    private void DeleteAllChild()
    {
        int i = parentTransform.childCount;
        while (i > 0)
        {
            i--;
            Destroy(parentTransform.GetChild(i).gameObject);
        }
    }
}
