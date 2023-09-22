using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class ItemScriptable : ScriptableObject
{
    public int ID;
    public Sprite Image;
    public int stackSize;

    [Header("Discription:")]
    [Multiline]
    public string Discription;
}
