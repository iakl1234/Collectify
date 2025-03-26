using UnityEngine;

public class Item
{
    public string item_name;
    public string item_year;
    public string item_production;
    public string item_description;
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
    public Item(string name, string year, string production, string description)
    {
        this.item_name = name;
        this.item_year = year;
        this.item_production = production;
        this.item_description = description;
    }
}
