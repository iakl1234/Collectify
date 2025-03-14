using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AllCollection : Page
{
    public GameObject CollectionContainer;
    public GameObject Scroller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Мои коллекции";
        footerActive = true;
    }
    public void OpenCreateCollection()
    {
        Main.main.OpenCreateColletion();
    }
    private void OnEnable()
    {

        AsyncOnEnable();

    }

    private async void AsyncOnEnable()
    {
        Main.main.StartSetting(buttonBackActive, labelActive, labelText, footerActive);
        // Проходим по всем дочерним объектам и удаляем их
        foreach (Transform child in CollectionContainer.transform)
        {
            Destroy(child.gameObject);
        }
        await FirestoreManager.Instance.LoadUserCollections();
        UppdateSize();
    }
    public void UppdateSize()
    {
        foreach (var collection in Main.main.CollectionsList)
        {
            GameObject Prefab = Instantiate(Resources.Load<GameObject>("Prefabs/Collection"), CollectionContainer.transform.position, Quaternion.identity);
            Prefab.GetComponent<CollectionEvent>().Init(collection);
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
