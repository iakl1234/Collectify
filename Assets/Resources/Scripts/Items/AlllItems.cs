using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AllItems: Page
{
    public GameObject CollectionContainer;
    public GameObject Scroller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = Main.main.collection.collection_name;
        footerActive = true;
        buttonDeleteActive = true;
    }
    public void OpenCreateCollection()
    {
        Main.main.OpenCreateColletion();
    }

    private void OnEnable()
    {
        Main.main.StartSetting(buttonBackActive, labelActive, labelText, footerActive, buttonDeleteActive);
        // Проходим по всем дочерним объектам и удаляем их
        foreach (Transform child in CollectionContainer.transform)
        {
            Destroy(child.gameObject);
        }
        UppdateSize();

    }
    public void UppdateSize()
    {
        foreach (var collection in Main.main.ItemList)
        {
            GameObject Prefab = Instantiate(Resources.Load<GameObject>("Prefabs/Item"), CollectionContainer.transform.position, Quaternion.identity);
            Prefab.GetComponent<ItemEvent>().Init(collection);
            Prefab.transform.SetParent(CollectionContainer.transform, false);
        }


        int rows = Mathf.CeilToInt((Main.main.CollectionsList.Count+1) / 2);

        float height = rows * 500;

        CollectionContainer.GetComponent<RectTransform>().sizeDelta= new Vector2(CollectionContainer.GetComponent<RectTransform>().sizeDelta.x, height);
        //GameObject empty = new GameObject("Empty");
        //empty.AddComponent<RectTransform>();
        //empty.transform.SetParent(CollectionContainer.transform, false);

    }
}
