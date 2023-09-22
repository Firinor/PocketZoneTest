using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOff : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;

    public void SetInventoryOff()
    {
        inventory.SetActive(false);
    }
}
