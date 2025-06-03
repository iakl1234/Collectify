using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileEditController : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField usernameInput;
    public TMP_InputField emailInput;
    public Button saveButton;

    void OnEnable()
    {
        // ��������� ���� �������� �������
        if (Main.main == null) return;

        nameInput.text = Main.main.userData.Name;
        usernameInput.text = Main.main.userData.Username;
        emailInput.text = Main.main.userData.Email;

        // ����������� ������ ����������
        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(OnSaveClicked);
    }

    private void OnSaveClicked()
    {
        if (Main.main == null) return;

        // ��������� ����� ������
        Main.main.userData.Name = nameInput.text;
        Main.main.userData.Username = usernameInput.text;
        Main.main.userData.Email = emailInput.text;

        // ��������� ������� � �������
        Main.main.OpenProfile();
    }
}