using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCollection : Page
{
    public TMP_InputField Name;
    //public TextMeshProUGUI Notification;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "Мои коллекции";
        footerActive=true;
    }
    public async Task CreateCollectionButton()
    {
        Debug.Log("Введены правильно");
        if (Name.text != "")
        {
            Debug.Log("Введены правильно");
            //Notification.enabled = false;
            Collection newCollection = new Collection(Name.text);
            await FirestoreManager.Instance.AddNewCollection(newCollection);
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
