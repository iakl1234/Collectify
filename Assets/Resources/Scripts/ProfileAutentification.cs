using TMPro;
using UnityEngine;

public class ProfileAutentification : Page
{
    public TMP_InputField Email;
    public TMP_InputField Password;
    public TextMeshProUGUI Notification;
    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Авторизация";
        footerActive = false;

    }
    public void Autorization()
    {

        if (Email.text == "" || Password.text=="")
        {
            Notification.enabled = true;
        }
        else
        {
            //Debug.Log(Email.text);
            FirestoreManager.Instance.SignInUser(Email.text, Password.text);
            //Main.main.user.authorized = true;
            //Main.main.OpenEntrance();
        }
    }

}
