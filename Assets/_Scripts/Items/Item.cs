using System;

[Serializable]
public class Item
{
    public int ID;
    public string Discription;
    public int Count;

    public Item(ItemScriptable scriptable)
    {
        ID = scriptable.ID;
        Discription = scriptable.Discription;
        Count = 1;
    }
}