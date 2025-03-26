using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CreateItem : Page
{
    public TMP_InputField Name;
    public TMP_InputField Year;
    public TMP_InputField Production;
    public TMP_InputField Description;
    //public TextMeshProUGUI Notification;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "Мои предметы";
        footerActive=true;
    }
    public void CreateCollectionButton()
    {
        AsyncCreateCollectionButton();
    }
    public async Task AsyncCreateCollectionButton()
    {
        Debug.Log("Введены правильно");
        if (Name.text != "")
        {
            Debug.Log("Введены правильно");
            //Notification.enabled = false;
            Item newItem = new Item(Name.text, Year.text, Production.text, Description.text);
            await FirestoreManager.Instance.AddNewItem(newItem, Main.main.collection.id);
            //Main.main.CollectionsList.Add(newCollection);
            Main.main.Back();
        }
        else
        {
            Debug.Log("Введены неправильно");
            //Notification.enabled = true;
        }

    }
}
