using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Inventory
{
    private List<Item> items = new();
    public IReadOnlyList<Item> Items => items;

    public void AddItem(ItemScriptable scriptable)
    {
        var itemStack = from i in items
                        where i.ID == scriptable.ID
                        where i.Count < scriptable.stackSize
                        select i;

        if(itemStack.Any())
        {
            itemStack.First().Count++;
        }
        else
        {
            items.Add(new Item(scriptable));
        }

        SaveLoadManager.Save();
    }

    public void RemoveItem(Item item)
    {
        var itemStack = from i in items
                        where i == item
                        select i;

        if (itemStack.Any())
        {
            items.Remove(itemStack.First());
        }
        else
        {
            throw new Exception("Attempt to delete a non-existent item!");
        }

        SaveLoadManager.Save();
    }
}