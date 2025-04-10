using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEvent : MonoBehaviour
{
    Item Item;
    public TextMeshProUGUI Name;
    public Button Button;
    public Button Button_edit;  // Добавляем поле для кнопки редактирования
    public void Init(Item newCollection)
    {
        this.Name.text = newCollection.item_name;
        Item = newCollection;
    }
    public void OpenItemInfo()
    {
        Main.main.OpenItemInfo(Item);
    }
    public void OnButton_editClicked()
    {
        Main.main.OpenEditItem(Item);
    }
}
