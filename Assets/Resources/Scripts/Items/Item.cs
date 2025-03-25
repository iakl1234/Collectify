using UnityEngine;

public class Item
{
    public string item_name;
    public string id;

    public Item(string name, string id)
    {
        this.item_name = name;
        this.id = id;
    }
    public Item(string name)
    {
        this.item_name = name;
    }
}
