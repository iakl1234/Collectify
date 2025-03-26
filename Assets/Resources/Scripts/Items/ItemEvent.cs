using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEvent : MonoBehaviour
{
    Item Item;
    public TextMeshProUGUI Name;
    public Button Button;
    public void Init(Item newCollection)
    {
        this.Name.text = newCollection.item_name;
        Item = newCollection;
    }
    public void OpenItemInfo()
    {
        Main.main.OpenItemInfo(Item);
    }
}
