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
    public User user = new User();
    public string UserName;


    public List<Collection> CollectionsList;
    public List<Item> ItemList;
    void Awake()
    {
        main = this;
        CollectionsList = new List<Collection>();
        ItemList = new List<Item>();
        firestoreManager = new FirestoreManager();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Принудительная инициализация UI
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(Main_container.GetComponent<RectTransform>());

        OpenProfileAutentification();
    }
    public void OpenProfileAutentification()
    {
        // Всегда очищаем стек при открытии авторизации
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
        openPrefab("AllItems", false, Main_container);

    }

    public void OpenItemInfo(Item item)
    {
        this.item = item;
        openPrefab("ItemInfo", false, Main_container);
    }
    public void OpenProfile()
    {
        openPrefab("Profile", true, Main_container);
    }
    public void OpenProfileedit()
    {
        openPrefab("Profileedit", true, Main_container);
    }
    public void OpenProfilequit()
    {
        openPrefab("Profilequit", true, Main_container);
    }
    public void OpenProfilequit1()
    {
        // Выход - очищаем весь стек и открываем авторизацию
        OpenProfileAutentification();
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



    public void StartSetting(bool activeBack, bool activeLabel, string labelText = "", bool activeFooter = true, bool activeDelete = false)
    {
        Button_back.SetActive(activeBack);
        Label.enabled = activeLabel;
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
    // Main.cs

    public void Back()
    {
        try
        {
            if (prefabQueue.Count > 1) // Не позволяем удалить последний элемент
            {
                deleteLastContainers();
            }
            else
            {
                Debug.LogWarning("Can't go back - only one screen in stack");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Back error: {e.Message}");
        }
    }

    private void deleteLastContainers()
    {
        if (prefabQueue.Count == 0)
        {
            Debug.LogWarning("Stack is empty - nothing to delete");
            return;
        }

        // Удаляем текущий активный префаб
        GameObject top = prefabQueue.Pop();
        if (top != null) Destroy(top);

        // Активируем предыдущий префаб
        if (prefabQueue.Count > 0)
        {
            GameObject previous = prefabQueue.Peek();
            previous.SetActive(true);
            Debug.Log($"Activated previous screen: {previous.name}");
        }
    }

    private void deleteAllContainers()
    {
        Debug.Log($"Clearing all containers ({prefabQueue.Count} items)");

        while (prefabQueue.Count > 0)
        {
            GameObject obj = prefabQueue.Pop();
            if (obj != null)
            {
                Debug.Log($"Destroying: {obj.name}");
                Destroy(obj);
            }
        }
    }

    private void openPrefab(string prefabName, bool close, GameObject container)
    {
        Debug.Log($"Opening: {prefabName}, close: {close}, container: {container.name}");

        try
        {
            // Очистка предыдущих префабов
            if (close)
            {
                deleteAllContainers();
            }
            else if (prefabQueue.Count > 0)
            {
                GameObject top = prefabQueue.Peek();
                if (top != null) top.SetActive(false);
            }

            // Загрузка префаба
            string path = "Prefabs/" + prefabName;
            GameObject prefabObj = Resources.Load<GameObject>(path);

            if (prefabObj == null)
            {
                Debug.LogError($"Prefab not found: {path}");
                return;
            }

            // Создание экземпляра
            Prefab = Instantiate(prefabObj, container.transform);
            prefabQueue.Push(Prefab);

            // КРИТИЧЕСКИ ВАЖНЫЕ НАСТРОЙКИ ДЛЯ ОТОБРАЖЕНИЯ
            RectTransform rt = Prefab.GetComponent<RectTransform>();
            if (rt != null)
            {
                // Настройка для полного растяжения
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.localScale = Vector3.one;

                Debug.Log("RectTransform set to full stretch");
            }

            // Принудительное обновление UI
            LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
            Canvas.ForceUpdateCanvases();

            Debug.Log($"Successfully opened: {prefabName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error opening prefab: {e.Message}");
        }
    }

}