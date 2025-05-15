using TMPro;
using UnityEngine;

public class Profile : Page
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI Email;
    private User user;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Профиль";
        footerActive = true;
        buttonDeleteActive = false;
        user = Main.main.user;
        Name.text = user.Login;
        UserName.text = user.Username;
        Email.text = user.Email;
    }



}
