using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditCollection : Page
{
    public TMP_InputField Name;
    public TextMeshProUGUI NameText;
    private Collection collectionToEdit;
    //public TextMeshProUGUI Notification;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = true;
        labelActive = true;
        labelText = "Мои коллекции";
        footerActive=true;
        collectionToEdit = Main.main.collection;
        NameText.text = Main.main.collection.collection_name;
    }
    public void Init(Collection collection)
    {
        Debug.Log(collection.collection_name);
        collectionToEdit = collection;
        Name.text = collection.collection_name;  // Устанавливаем текущее название коллекции в поле ввода
    }
    public void EditCollectionButton()
    {
        AsyncEditCollectionButton();
    }
    public async Task AsyncEditCollectionButton()
    {
        Debug.Log("Редактирование коллекции");

        if (Name.text != "")
        {
            Debug.Log("Имя коллекции изменено");
            collectionToEdit.collection_name = Name.text;  // Обновляем название коллекции

            // Отправляем обновления в базу данных
            await FirestoreManager.Instance.UpdateCollection(collectionToEdit);  

            // Возвращаемся назад
            Main.main.Back();
        }
        else
        {
            Debug.Log("Название коллекции не может быть пустым");
            // Можно добавить уведомление о том, что имя не может быть пустым
        }
    }
}
