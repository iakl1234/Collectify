using TMPro;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text usernameText;
    public TMP_Text emailText;

    void OnEnable()
    {
        UpdateProfileDisplay();
    }

    public void UpdateProfileDisplay()
    {
        if (Main.main == null) return;

        nameText.text = Main.main.userData.Name;
        usernameText.text = Main.main.userData.Username;
        emailText.text = Main.main.userData.Email;
    }
}