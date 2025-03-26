using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : Page
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
        labelText = Main.main.item.item_name;
        footerActive = true;
        buttonDeleteActive = true;
        Main.main.Button_delete.GetComponent<Button>().onClick.RemoveAllListeners();
        Main.main.Button_delete.GetComponent<Button>().onClick.AddListener(Main.main.DeleteItem);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
