using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using Google.MiniJSON;
using UnityEngine;

public class FirestoreManager : MonoBehaviour
{

    const string url = "https://collectorsapp-9330a.firestore.app";
    private FirebaseAuth auth;
    public static FirestoreManager Instance;
    private FirebaseFirestore firestore;
    void Awake()
    {
        Instance = this;
        firestore = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    public async Task AddNewCollection(Collection collection)
    {
        try
        {
            // Ссылка на подколлекцию пользователя
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // Создаем новый документ с автоматически сгенерированным ID
            Dictionary<string, object> newCollection = new Dictionary<string, object>
            {
                { "Name", collection.collection_name },
                { "CreatedAt", FieldValue.ServerTimestamp } // Добавляем метку времени
            };

            // Добавляем новый документ в коллекцию
            DocumentReference addedDocRef = await collectionsRef.AddAsync(newCollection);

            Debug.Log($"Новая коллекция добавлена с ID: {addedDocRef.Id}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при добавлении коллекции: {e.Message}");
        }
    }
    public async Task LoadUserCollections()
    {

        try
        {
            Main.main.CollectionsList.Clear();
            // Ссылка на подколлекцию пользователя
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // Получение всех документов
            QuerySnapshot snapshot = await collectionsRef.GetSnapshotAsync();

            // Обработка результатов
            List<Dictionary<string, object>> collections = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Collection newCollection = new Collection(document.GetValue<string>("Name"));
                Main.main.CollectionsList.Add(newCollection);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка: {e.Message}");
        }
    }

}