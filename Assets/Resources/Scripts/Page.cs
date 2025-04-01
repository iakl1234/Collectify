using UnityEngine;

public class Page : MonoBehaviour
{
    internal bool buttonBackActive;
    internal bool labelActive;
    internal bool footerActive;
    internal bool buttonDeleteActive=false;
    internal string labelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Main.main.StartSetting(buttonBackActive, labelActive, labelText, footerActive, buttonDeleteActive);
        //Main.main.Button_new.onClick.RemoveAllListeners();
        //Main.main.Button_new.onClick.AddListener(() => onClick());

    }
    private void OnEnable()
    {
        Main.main.StartSetting(buttonBackActive, labelActive, labelText, footerActive, buttonDeleteActive);
        //Main.main.Button_new.onClick.RemoveAllListeners();
        //Main.main.Button_new.onClick.AddListener(() => onClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal virtual void onClick()
    {
    }

    protected void OpenModal(GameObject modalPrefab)
    {
        modalPrefab.transform.SetParent(Main.main.Main_container.transform, false);
        modalPrefab.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
