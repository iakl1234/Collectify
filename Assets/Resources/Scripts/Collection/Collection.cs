using UnityEngine;

public class Collection
{
    public string collection_name;
    public string id;

    public Collection(string name, string id)
    {
        this.collection_name = name;
        this.id = id;
    }
    public Collection(string name)
    {
        this.collection_name = name;
    }
}
