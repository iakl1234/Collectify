using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;


public class Main : MonoBehaviour
{
    public static Main main;
    public GameObject Main_container;
    public GameObject Attention_container;
    public GameObject Prefab;
    public TextMeshProUGUI Label;
    public GameObject Button_back;
    public GameObject Footer;
    public GameObject Button_delete_Image;
    public Button Button_delete;
    public Collection collection;
    public Item item;
    //public GameObject Button_new_container;
    //public Button Button_new;
    private Stack<GameObject> prefabQueue = new Stack<GameObject>();
    //public User user=new User();
    public FirestoreManager firestoreManager;
    public User user=new User();
    public string UserName;


    public List<Collection> CollectionsList;
    public List<Item> ItemList;
    void Awake()
    {
        main = this;
        CollectionsList=new List<Collection>();
        ItemList = new List<Item>();
        firestoreManager = new FirestoreManager();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenProfileAutentification();
        //OpenAllCollection();
    }
    public void OpenProfileAutentification()
    {
        openPrefab("ProfileAutentification", true, Main_container);
    }
    public void OpenRegistration()
    {
        openPrefab("ProfileRegistration", true, Main_container);
    }
    public void OpenCreateColletion()
    {
        openPrefab("CreateCollection", false, Main_container);
    }
    public void OpenEditCollection(Collection collection)
    {
        this.collection = collection;
        
        openPrefab("EditCollection", false, Main_container);
    }

    public void OpenCreateItem()
    {
        openPrefab("CreateItem", false, Main_container);
    }
    public void OpenEditItem(Item item)
    {
        this.item = item;
        
        openPrefab("EditItem", false, Main_container);
    }
    public void OpenAllCollection()
    {
        openPrefab("AllCollection", true, Main_container);
    }
    public void OpenAllItems(Collection collection)
    {
        this.collection = collection;
        openPrefab("AllItems",false, Main_container);
        
    }

    public void OpenItemInfo(Item item)
    {
        this.item = item;
        openPrefab("ItemInfo", false, Main_container);
    }


    public void DeleteWindow()
    {
        openPrefab("ConfirmDelete", true, Attention_container);
    }
    public void DeleteCollection()
    {
        openPrefab("ConfirmDelete", false, Attention_container);
    }

    public void DeleteCollectionConfirm()
    {
        AsyncDeleteCollection();
    }

    public async Task AsyncDeleteCollection()
    {
        await FirestoreManager.Instance.DeleteDocumentAsync(collection);
        Back();
    }
    public void DeleteItem()
    {
        openPrefab("ConfirmDeleteItem", false, Attention_container);
    }

    // ����� ��� ������������� ��������
    public void DeleteItemConfirm()
    {
        AsyncDeleteItem();
    }

    // ����������� ��������
    public async Task AsyncDeleteItem()
    {
        await FirestoreManager.Instance.DeleteItemAsync(item);
        Back();
    }



    public void StartSetting(bool activeBack, bool activeLabel,string labelText="", bool activeFooter=true, bool activeDelete=false)
    {
        Button_back.SetActive(activeBack);
        Label.enabled=activeLabel;
        if (activeLabel)
        {
            Label.text = labelText;
        }
        Footer.SetActive(activeFooter);
        Button_delete_Image.SetActive(activeDelete);
    }

    //public void OpenIventInfo(Event newEvent)
    //{
    //    openPrefab("EventInfo", false);
    //    Rename_header(newEvent.event_name);
    //    Prefab.GetComponent<IventInfo>().Init(newEvent);

    //}
    //public void Open_Registration()
    //{
    //    openPrefab("ProfileRegistration", false);
    //}
    //public void Open_Authorization()
    //{
    //    openPrefab("ProfileAutentification", false);
    //}
    public void Rename_header(string name)
    {
        Label.text = name;
    }
    public void Back()
    {
        deleteLastContainers();
    }
    private void deleteLastContainers()
    {
        Destroy(prefabQueue.Pop());
        prefabQueue.Peek().gameObject.SetActive(true);
    }
    private void deleteAllContainers()
    {
        foreach (GameObject container in prefabQueue) Destroy(container);
    }
    private void openPrefab(string prefabName,bool close,GameObject container)
    {
        if (close) deleteAllContainers(); else try { prefabQueue.Peek().gameObject.SetActive(false); } catch { };
        Prefab = Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabName), container.transform.position, Quaternion.identity);
        Prefab.transform.SetParent(container.transform, false);
        prefabQueue.Push(Prefab);
        Prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }

}
