using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class EditItem : Page
{
    public TMP_InputField Name;
    public TMP_InputField Year;
    public TMP_InputField Production;
    public TMP_InputField Description;

    private Item itemToEdit;

    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "Редактировать предмет";
        footerActive = true;
    }

    public void Init(Item item)
    {
        itemToEdit = item;

        Name.text = item.item_name;
        Year.text = item.item_year;
        Production.text = item.item_production;
        Description.text = item.item_description;
    }

    public void EditItemButton()
    {
        AsyncEditItemButton();
    }

    public async Task AsyncEditItemButton()
    {
        Debug.Log("Редактирование предмета...");

        if (!string.IsNullOrEmpty(Name.text))
        {
            itemToEdit.item_name = Name.text;
            itemToEdit.item_year = Year.text;
            itemToEdit.item_production = Production.text;
            itemToEdit.item_description = Description.text;

            await FirestoreManager.Instance.UpdateItem(itemToEdit, Main.main.collection.id);

            Main.main.Back();
        }
        else
        {
            Debug.Log("Имя предмета не может быть пустым");
            // Здесь можно показать предупреждение на экране
        }
    }
}

