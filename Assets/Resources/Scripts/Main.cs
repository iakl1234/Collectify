using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Main : MonoBehaviour
{
    public static Main main;
    public GameObject Main_container;
    public GameObject Prefab;
    public TextMeshProUGUI Label;
    public GameObject Button_back;
    public GameObject Footer;
    public GameObject Button_delete;
    public string name_collection;
    //public GameObject Button_new_container;
    //public Button Button_new;
    private Stack<GameObject> prefabQueue = new Stack<GameObject>();
    //public User user=new User();
    public FirestoreManager firestoreManager;



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
        OpenAllCollection();
    }
    public void OpenCreateColletion()
    {
        openPrefab("CreateCollection", false);
    }
    public void OpenAllCollection()
    {
        openPrefab("AllCollection", true);
    }
    public void OpenAllItems(string name)
    {
        name_collection = name;
        openPrefab("AllItems",false);
        
    }

    //public void OpenEntrance()
    //{
    //    if (user.authorized) openPrefab("Profile", true);  else openPrefab("Entrance", true);

    //}
    public void StartSetting(bool activeBack, bool activeLabel,string labelText="", bool activeFooter=true)
    {
        Button_back.SetActive(activeBack);
        Label.enabled=activeLabel;
        if (activeLabel)
        {
            Label.text = labelText;
        }
        Footer.SetActive(activeFooter);
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
    private void openPrefab(string prefabName,bool close)
    {
        if (close) deleteAllContainers(); else try { prefabQueue.Peek().gameObject.SetActive(false); } catch { };
        Prefab = Instantiate(Resources.Load<GameObject>("Prefabs/" + prefabName), Main_container.transform.position, Quaternion.identity);
        Prefab.transform.SetParent(Main_container.transform, false);
        prefabQueue.Push(Prefab);
        Prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
    }

}
