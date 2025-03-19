using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionEvent : MonoBehaviour
{
    Collection Collection;
    public TextMeshProUGUI Name;
    public Button Button;
    public void Init(Collection newCollection)
    {
        this.Name.text = newCollection.collection_name;
        Collection = newCollection;
    }

    public void OnButtonClicked()
    {
        //ClearContainer();
        Main.main.OpenAllItems(Collection);
    }

    private void ClearContainer()
    {
        GameObject mainContainer = Main.main.Main_container;

        Transform mainTransform = mainContainer.transform;

        // Проходим по всем дочерним объектам Main
        foreach (Transform child in mainTransform)
        {
            // Если имя дочернего объекта содержит "Collection", удаляем его
            if (child.name.Contains("Collection(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
