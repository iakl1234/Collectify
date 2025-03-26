using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AllItems: Page
{
    public GameObject ItemContainer;
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
    public void OpenCreateItem()
    {
        Main.main.OpenCreateItem();
    }

    private void OnEnable()
    {

        AsyncOnEnable();

    }

    private async void AsyncOnEnable()
    {

        Main.main.StartSetting(buttonBackActive, labelActive, labelText, footerActive, buttonDeleteActive);
        // Проходим по всем дочерним объектам и удаляем их
        foreach (Transform child in ItemContainer.transform)
        {
            Destroy(child.gameObject);
        }
        await FirestoreManager.Instance.LoadUserItems();
        UppdateSize();
    }
    public void UppdateSize()
    {
        foreach (var item in Main.main.ItemList)
        {
            GameObject Prefab = Instantiate(Resources.Load<GameObject>("Prefabs/Item"), ItemContainer.transform.position, Quaternion.identity);
            Prefab.GetComponent<ItemEvent>().Init(item);
            Prefab.transform.SetParent(ItemContainer.transform, false);
        }


        int rows = Mathf.CeilToInt((Main.main.ItemList.Count+1) / 2);

        float height = rows * 500;

        ItemContainer.GetComponent<RectTransform>().sizeDelta= new Vector2(ItemContainer.GetComponent<RectTransform>().sizeDelta.x, height);
        //GameObject empty = new GameObject("Empty");
        //empty.AddComponent<RectTransform>();
        //empty.transform.SetParent(CollectionContainer.transform, false);

    }
}
