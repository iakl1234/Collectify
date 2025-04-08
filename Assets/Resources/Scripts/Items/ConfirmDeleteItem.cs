using UnityEngine;
using UnityEngine.UI;

public class ConfirmDeleteItem : Page
{
    public Button confirmButton;
    public Button cancelButton;

    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Удаление предмета";
        footerActive = false;
    }

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
    }

    private void OnConfirm()
    {
        Main.main.DeleteItemConfirm();
        Main.main.Back();
    }

    private void OnCancel()
    {
        Main.main.Back();
    }
}