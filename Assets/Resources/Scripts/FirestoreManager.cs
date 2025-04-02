using System;
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

    public async Task AddNewItem(Item item, string collectionId)
    {
        try
        {
            // Ссылка на конкретную коллекцию пользователя
            DocumentReference collectionRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collectionId);

            // Ссылка на подколлекцию Items внутри коллекции
            CollectionReference itemsRef = collectionRef.Collection("Items");

            // Создаем новый документ предмета
            Dictionary<string, object> newItem = new Dictionary<string, object>
        {
            { "Name", item.item_name },
            { "Year", item.item_year },
            { "Production", item.item_production },
            { "Description", item.item_description },
            { "CreatedAt", FieldValue.ServerTimestamp },
            // Добавьте другие поля предмета по необходимости
        };

            // Добавляем новый документ в подколлекцию Items
            DocumentReference addedItemRef = await itemsRef.AddAsync(newItem);

            Debug.Log($"Новый предмет добавлен с ID: {addedItemRef.Id} в коллекцию {collectionId}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при добавлении предмета: {e.Message}");
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
                Collection newCollection = new Collection(document.GetValue<string>("Name"),document.Id);
                Main.main.CollectionsList.Add(newCollection);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка: {e.Message}");
        }
    }
    public async Task LoadUserItems()
    {

        try
        {
            Main.main.ItemList.Clear();
            // Ссылка на подколлекцию пользователя
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName).Collection("Collections").Document(Main.main.collection.id);
            CollectionReference itemsRef = userRef.Collection("Items");

            // Получение всех документов
            QuerySnapshot snapshot = await itemsRef.GetSnapshotAsync();

            // Обработка результатов
            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Item newItem = new Item(document.GetValue<string>("Name"),document.Id);
                Main.main.ItemList.Add(newItem);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка: {e.Message}");
        }
    }

    public async Task DeleteDocumentAsync(Collection collections)
    {
        try
        {
            // Получаем ссылку на документ
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collections.id);

            // Проверяем существование документа
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("Документ не найден");
                return;
            }

            // Удаляем документ
            await docRef.DeleteAsync();
            Console.WriteLine($"Документ {collections.collection_name} успешно удален");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления: {ex.Message}");
        }
    }


    public async Task DeleteItemAsync(Item item)
    {
        try
        {
            // Получаем ссылку на документ
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(Main.main.collection.id).Collection("Items").Document(item.id);

            // Проверяем существование документа
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("Документ не найден");
                return;
            }

            // Удаляем документ
            await docRef.DeleteAsync();
            Console.WriteLine($"Документ {item.item_name} успешно удален");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка удаления: {ex.Message}");
        }
    }

    public void RegisterUser(string email, string password)
    {

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCompleted)
            {
                Debug.Log("zzzzzzzzzzzzz");

            }
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Пользователь успешно зарегистрирован
            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            Main.main.UserName = newUser.UserId;
            Main.main.OpenAllCollection();
        });
    }
    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                try
                {
                    RegisterUser(email, password);
                }
                catch { Debug.Log("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception); }
                
                return;
            }

            // Пользователь успешно вошел в систему
            FirebaseUser user = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
            Main.main.UserName = user.UserId;
            Main.main.OpenAllCollection();
            
        });
    }




}