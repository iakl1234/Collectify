using TMPro;
using UnityEngine;

public class ProfileRegistration:Page
{
    public TMP_InputField Email;
    public TMP_InputField Password;
    public TMP_InputField ConfirmationPassword;
    public TMP_InputField Login;
    public TMP_InputField UserName;

    public TextMeshProUGUI Notification;
    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Регистрация";
        footerActive = false;

    }
    public void Registration()
    {

        if (Email.text == "" || Password.text == "")
        {
            //Notification.enabled = true;
        }
        else
        {
            //Debug.Log(Email.text);
            FirestoreManager.Instance.RegisterUser(Email.text, Password.text, UserName.text,Login.text);
            //Main.main.user.authorized = true;
            //Main.main.OpenEntrance();
        }
    }
    public void OpenAutorization()
    {

        Main.main.OpenProfileAutentification();
    }
}
