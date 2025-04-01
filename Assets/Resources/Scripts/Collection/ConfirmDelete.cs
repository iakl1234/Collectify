// ConfirmDelete.cs
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ConfirmDelete : Page
{
    public Button confirmButton;
    public Button cancelButton;
    private System.Action onConfirm;

    private void Awake()
    {
        buttonBackActive = false;
        labelActive = true;
        labelText = "Мои коллекции";
        footerActive = true;
    }

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void Init(System.Action confirmAction)
    {
        this.onConfirm = confirmAction;
    }

    private void OnConfirm()
    {
        Main.main.DeleteCollectionConfirm();
        Main.main.Back();
    }

    private void OnCancel()
    {
        Main.main.Back();
    }


}