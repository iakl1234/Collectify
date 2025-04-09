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
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // ������� ����� �������� � ������������� ��������������� ID
            Dictionary<string, object> newCollection = new Dictionary<string, object>
            {
                { "Name", collection.collection_name },
                { "CreatedAt", FieldValue.ServerTimestamp } // ��������� ����� �������
            };

            // ��������� ����� �������� � ���������
            DocumentReference addedDocRef = await collectionsRef.AddAsync(newCollection);

            Debug.Log($"����� ��������� ��������� � ID: {addedDocRef.Id}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������ ��� ���������� ���������: {e.Message}");
        }
    }
    public async Task UpdateCollection(Collection collection)
    {
        try
        {
            DocumentReference collectionRef = firestore.Collection("Users").Document(Main.main.UserName).Collection("Collections").Document(collection.id);
            Dictionary<string, object> updatedCollection = new Dictionary<string, object>
            {
                { "Name", collection.collection_name },  // Обновляем имя коллекции
                { "UpdatedAt", FieldValue.ServerTimestamp }  // Добавляем отметку времени обновления
            };
         await collectionRef.UpdateAsync(updatedCollection);

         Debug.Log($"Коллекция {collection.collection_name} обновлена.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Ошибка при обновлении коллекции: {e.Message}"); 
        }
    }

    public async Task AddNewItem(Item item, string collectionId)
    {
        try
        {
            // ������ �� ���������� ��������� ������������
            DocumentReference collectionRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collectionId);

            // ������ �� ������������ Items ������ ���������
            CollectionReference itemsRef = collectionRef.Collection("Items");

            // ������� ����� �������� ��������
            Dictionary<string, object> newItem = new Dictionary<string, object>
        {
            { "Name", item.item_name },
            { "Year", item.item_year },
            { "Production", item.item_production },
            { "Description", item.item_description },
            { "CreatedAt", FieldValue.ServerTimestamp },
            // �������� ������ ���� �������� �� �������������
        };

            // ��������� ����� �������� � ������������ Items
            DocumentReference addedItemRef = await itemsRef.AddAsync(newItem);

            Debug.Log($"����� ������� �������� � ID: {addedItemRef.Id} � ��������� {collectionId}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������ ��� ���������� ��������: {e.Message}");
        }
    }

    public async Task LoadUserCollections()
    {

        try
        {
            Main.main.CollectionsList.Clear();
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName);
            CollectionReference collectionsRef = userRef.Collection("Collections");

            // ��������� ���� ����������
            QuerySnapshot snapshot = await collectionsRef.GetSnapshotAsync();

            // ��������� �����������
            List<Dictionary<string, object>> collections = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Collection newCollection = new Collection(document.GetValue<string>("Name"),document.Id);
                Main.main.CollectionsList.Add(newCollection);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������: {e.Message}");
        }
    }
    public async Task LoadUserItems()
    {

        try
        {
            Main.main.ItemList.Clear();
            // ������ �� ������������ ������������
            DocumentReference userRef = firestore.Collection("Users").Document(Main.main.UserName).Collection("Collections").Document(Main.main.collection.id);
            CollectionReference itemsRef = userRef.Collection("Items");

            // ��������� ���� ����������
            QuerySnapshot snapshot = await itemsRef.GetSnapshotAsync();

            // ��������� �����������
            List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Item newItem = new Item(document.GetValue<string>("Name"),document.Id);
                Main.main.ItemList.Add(newItem);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"������: {e.Message}");
        }
    }

    public async Task DeleteDocumentAsync(Collection collections)
    {
        try
        {
            // �������� ������ �� ��������
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(collections.id);

            // ��������� ������������� ���������
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("�������� �� ������");
                return;
            }

            // ������� ��������
            await docRef.DeleteAsync();
            Console.WriteLine($"�������� {collections.collection_name} ������� ������");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��������: {ex.Message}");
        }
    }


    public async Task DeleteItemAsync(Item item)
    {
        try
        {
            // �������� ������ �� ��������
            DocumentReference docRef = firestore
                .Collection("Users")
                .Document(Main.main.UserName)
                .Collection("Collections")
                .Document(Main.main.collection.id).Collection("Items").Document(item.id);

            // ��������� ������������� ���������
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                Console.WriteLine("�������� �� ������");
                return;
            }

            // ������� ��������
            await docRef.DeleteAsync();
            Console.WriteLine($"�������� {item.item_name} ������� ������");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"������ ��������: {ex.Message}");
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

            // ������������ ������� ���������������
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

            // ������������ ������� ����� � �������
            FirebaseUser user = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
            Main.main.UserName = user.UserId;
            Main.main.OpenAllCollection();
            
        });
    }




}